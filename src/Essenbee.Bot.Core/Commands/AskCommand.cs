using Essenbee.Bot.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Essenbee.Bot.Core.Commands
{
    public class AskCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName => "ask";
        public string HelpText => "The !ask command uses the experimental Project Answer Search to try to answer your questions.";

        // ToDo: Move to configuration
        const string uriBase = "https://api.labs.cognitive.microsoft.com/answerSearch/v7.0/search";
        // Used to return news search results including relevant headers
        struct SearchResult
        {
            public HttpStatusCode statusCode;
            public String jsonResult;
            public Dictionary<String, String> relevantHeaders;
        }

        private enum AnswerType
        {
            Fact,
            Entity,
            Webpage
        };

        public void Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (Status != ItemStatus.Active) return;

            if (e.ArgsAsList.Count == 0)
            {
                chatClient.PostMessage(e.Channel, HelpText);
            }

            var searchTerm = new StringBuilder();
            foreach (var arg in e.ArgsAsList)
            {
                searchTerm.Append(arg);
                searchTerm.Append(" ");
            }

            var searchFor = searchTerm.ToString().Trim();
            var answerResponse = "I am sorry, but I do not know the answer ...";

            var result = BingLocalSearch(searchFor);

            if (result.statusCode == (HttpStatusCode)429) // Too Many Requests
            {
                answerResponse = "I am sorry, the !ask command is currently on cooldown. Please try again in a short while.";
            }

            if (result.statusCode == HttpStatusCode.OK)
            {
                var answerResult = JsonConvert.DeserializeObject<Answer>(result.jsonResult);

                if (answerResult?.Facts?.Value?.Length > 0)
                {
                    var answer = answerResult.Facts.Value[0].Description;
                    answer += GetDataAttribution(AnswerType.Fact, answerResult);
                    answerResponse = answer;
                }
                else if (answerResult?.Entities?.Value?.Length > 0)
                {
                    var answer = answerResult.Entities.Value[0].Description;
                    answer += GetDataAttribution(AnswerType.Entity, answerResult);
                    answerResponse = answer;
                }
                else if (answerResult?.WebPages?.Value?.Length > 0)
                {
                    var page = answerResult.WebPages.Value.FirstOrDefault(p => p.IsFamilyFriendly);

                    if (page != null)
                    {
                        var answer = page.Snippet;
                        var url = page.Url;
                        answerResponse = $"{answer} (Source: {url})";
                    }
                }
            }

            chatClient.PostMessage(e.Channel, answerResponse);
        }

        public bool ShoudExecute()
        {
            return Status == ItemStatus.Active;
        }

        private SearchResult BingLocalSearch(string searchQuery)
        {
            // Construct the URI of the search request
            var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(searchQuery) + "&mkt=en-us";

            // Send the Web request and get the response.
            var request = HttpWebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = Bot.ProjectAnswerKey;

            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

            // Create result object for return
            var searchResult = new SearchResult
            {
                statusCode = response.StatusCode
            };

            if (response.StatusCode == (HttpStatusCode)429) // Too Many Requests
            {
                return searchResult;
            }

            var json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            searchResult.jsonResult = json;
            searchResult.relevantHeaders = new Dictionary<String, String>();

            // Extract Bing HTTP headers
            foreach (String header in response.Headers)
            {
                if (header.StartsWith("BingAPIs-") || header.StartsWith("X-MSEdge-"))
                    searchResult.relevantHeaders[header] = response.Headers[header];
            }

            return searchResult;
        }

        private string GetDataAttribution(AnswerType type, Answer answerResult)
        {
            var retVal = string.Empty;

            if (type == AnswerType.Fact)
            {
                retVal = GetFactAttribution(answerResult);
            }

            if (type == AnswerType.Entity)
            {
                retVal = GetEntityAttribution(answerResult);
            }

            return retVal;
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

                retVal = $"\n{text}\t{url}";
            }
            else if (answerResult.Facts.Attributions != null)
            {
                text = answerResult.Facts.Attributions[0]?.ProviderDisplayName ?? string.Empty;
                url = answerResult.Facts.Attributions[0]?.SeeMoreUrl ?? string.Empty;

                retVal = $"\n{text}\t{url}";
            }

            return retVal;
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

                return $"\n{text}\t{url}\t{licence} {(licence != string.Empty ? licenceUrl : string.Empty)}";
            }

            return string.Empty;
        }
    }
}
