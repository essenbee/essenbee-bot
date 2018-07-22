using ConsoleTableExt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;

namespace Essenbee.Bot.Infra.CognitiveServices
{
    public class AnswerSearch
    {
        // ToDo: Move to configuration
        private const string UriBase = "https://api.labs.cognitive.microsoft.com/answerSearch/v7.0/search";
        private readonly string _apiKey;
        
        public AnswerSearch(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string GetAnswer(string searchFor)
        {
            var result = BingLocalSearch(searchFor);
            var answerResponse = "I am sorry, but I do not know the answer ...";

            if (result.StatusCode == (HttpStatusCode)429) // Too Many Requests
            {
                if (string.IsNullOrWhiteSpace(result.RetryAfter))
                {
                    result.RetryAfter = "several";
                }

                answerResponse = $"I am sorry, the !ask command is currently on cooldown. Please try again in {result.RetryAfter} seconds.";
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var answerResult = JsonConvert.DeserializeObject<Answer>(result.JsonResult);

                // If the query is for a fact such as a date or identifiable knowledge, the response can contain 
                // facts answers. Fact answers contain relevant results extracted from paragraphs in web documents.
                // If the query requests information about a person, place or thing, the response can contain an 
                // entities answer. Queries always return webpages, and the presence of facts and/or entities is 
                // query-dependent.

                if (answerResult?.Facts?.Value?.Length > 0)
                {
                    answerResponse = GetFact(answerResult);
                }
                else if (answerResult?.Entities?.Value?.Length > 0)
                {
                    answerResponse = GetEntity(answerResponse, answerResult);
                }
                else if (answerResult?.WebPages?.Value?.Length > 0)
                {
                    var page = answerResult.WebPages.Value.FirstOrDefault(p => p.IsFamilyFriendly);

                    if (page != null)
                    {
                        var answer = page.Snippet;
                        var url = page.Url;
                        answerResponse = $"{answer} (*Source*: {url})";
                        answerResponse += GetWebpageAttribution(page);
                    }
                }
            }

            return answerResponse;
        }
        
        private SearchResult BingLocalSearch(string searchQuery)
        {
            // Construct the URI of the search request
            var uriQuery = UriBase + "?q=" + Uri.EscapeDataString(searchQuery) + "&mkt=en-us&safeSearch=Strict";

            // Send the Web request and get the response.
            var request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = _apiKey;

            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

            // Create result object for return
            var searchResult = new SearchResult {
                StatusCode = response.StatusCode,
                RetryAfter = string.Empty,
            };

            if (response.StatusCode == (HttpStatusCode)429) // Too Many Requests
            {
                var headers = response.Headers;
                var retryAfter = headers.GetValues("Retry-After");
                if (retryAfter.Length > 0)
                {
                    searchResult.RetryAfter = retryAfter.First();
                }

                return searchResult;
            }

            var json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            searchResult.JsonResult = json;
            searchResult.RelevantHeaders = new Dictionary<String, String>();

            // Extract Bing HTTP headers
            foreach (String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.RelevantHeaders[header] = response.Headers[header];
            }

            return searchResult;
        }

        private string GetFact(Answer answerResult)
        {
            string answerResponse;
            // In some cases, facts can be returned in a tabular format ...
            if (answerResult.Facts.Value[0].RichCaption != null &&
                answerResult.Facts.Value[0].RichCaption.Type == "StructuredValue/TabularData")
            {
                answerResponse = BuildTabularData(answerResult);

                if (answerResult.Facts.Value[0].RichCaption?.SeeMoreUrl != null)
                {
                    answerResponse += $"\n*{answerResult.Facts.Value[0].RichCaption?.SeeMoreUrl?.Text}*: {answerResult.Facts.Value[0].RichCaption?.SeeMoreUrl?.Url}";
                }

                answerResponse += GetFactAttribution(answerResult);
            }
            else
            {
                var answer = answerResult.Facts.Value[0].Description ?? "See link below:";
                answer += GetFactAttribution(answerResult);
                answerResponse = answer;
            }

            return answerResponse;
        }

        private string GetFactAttribution(Answer answerResult)
        {
            var text = string.Empty;
            var url = string.Empty;
            var retVal = string.Empty;

            if (answerResult.Facts.ContractualRules != null)
            {
                text = answerResult.Facts.ContractualRules[0]?.Text ?? string.Empty;
                url = answerResult.Facts.ContractualRules[0]?.Url ?? string.Empty;

                retVal = $"\n\n{text}\t{url}";
            }
            else if (answerResult.Facts.Attributions != null)
            {
                text = answerResult.Facts.Attributions[0]?.ProviderDisplayName ?? string.Empty;
                url = answerResult.Facts.Attributions[0]?.SeeMoreUrl ?? string.Empty;

                retVal = $"\n\n{text}\t*See more*: {url}";
            }

            return retVal;
        }

        private string GetEntity(string answerResponse, Answer answerResult)
        {
            var dominantEntity = answerResult.Entities.Value.FirstOrDefault(x => x.EntityPresentationInfo?.EntityScenario == "DominantEntity");

            if (dominantEntity != null)
            {
                var answer = dominantEntity.Description;
                answer += GetEntityAttribution(answerResult);
                answerResponse = answer;
            }

            var disambiguations = answerResult.Entities.Value.Where(x => x.EntityPresentationInfo?.EntityScenario == "DisambiguationItem");

            var hints = disambiguations.ToList();
            if (hints.Any())
            {
                answerResponse += "\n\n*Disambiguation*: ";
                foreach (var hint in hints)
                {
                    if (hint?.EntityPresentationInfo?.EntityTypeHints != null &&
                        hint.EntityPresentationInfo.EntityTypeHints.Any())
                    {
                        answerResponse += hint.EntityPresentationInfo?.EntityTypeHints[0] + "; ";
                    }
                }
            }

            return answerResponse;
        }

        private string GetEntityAttribution(Answer answerResult)
        {
            var text = string.Empty;
            var url = string.Empty;
            var licence = string.Empty;
            var licenceUrl = string.Empty;

            if (answerResult.Entities.Value[0].ContractualRules != null)
            {
                foreach (var rule in answerResult.Entities.Value[0].ContractualRules
                    .Where(r => r.TargetPropertyName.Equals("description")))
                {
                    if (rule.Type.Equals("ContractualRules/LicenseAttribution"))
                    {
                        licence = rule.LicenseNotice;
                        licenceUrl = rule.License?.Url ?? string.Empty;
                    }

                    if (rule.Type.Equals("ContractualRules/LinkAttribution"))
                    {
                        text = rule.Text ?? string.Empty;
                        url = rule.Url ?? string.Empty;
                    }
                }

                return $"\n\n{text}\t{url}\t{licence} {(licence != string.Empty ? licenceUrl : string.Empty)}";
            }

            return string.Empty;
        }

        private string GetWebpageAttribution(WebPagesValue page)
        {
            var text = string.Empty;
            var licence = string.Empty;
            var licenceUrl = string.Empty;
            var retVal = string.Empty;

            if (page.SnippetAttribution != null)
            {
                text = page.SnippetAttribution.LicenseNotice ?? string.Empty;
                licence = page.SnippetAttribution?.License.Name ?? string.Empty;
                licenceUrl = page.SnippetAttribution?.License.Url ?? string.Empty;

                return $"\n\n{text}\t{licence} {(licence != string.Empty ? licenceUrl : string.Empty)}";
            }

            return retVal;
        }

        private string BuildTabularData(Answer answerResult)
        {
            var answerResponse = string.Empty;
            var tablularHeaders = answerResult.Facts.Value[0].RichCaption.Header;
            var tabularData = answerResult.Facts.Value[0].RichCaption.Rows;

            var table = new DataTable();

            foreach (var header in tablularHeaders)
            {
                if (string.IsNullOrWhiteSpace(header))
                {
                    table.Columns.Add(" ");
                }
                else
                {
                    table.Columns.Add(header);
                }
            }

            foreach (var row in tabularData)
            {
                var cells = row.Cells.Select(r => r.Text).ToArray();
                table.Rows.Add(cells);
            }

            var tableBuilder = ConsoleTableBuilder.From(table);
            answerResponse = "```\n" + tableBuilder.Export().ToString() + "\n```";

            return answerResponse;
        }
    }
}
