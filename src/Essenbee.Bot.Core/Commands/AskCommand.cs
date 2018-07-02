using Essenbee.Bot.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Essenbee.Bot.Core.Commands
{
    public class AskCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName => "ask";
        public string HelpText => "The !ask command uses the experimental Project Answer to try and answer questions.";

        const string uriBase = "https://api.labs.cognitive.microsoft.com/answerSearch/v7.0/search";
        // Used to return news search results including relevant headers
        struct SearchResult
        {
            public String jsonResult;
            public Dictionary<String, String> relevantHeaders;
        }

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

            var result = BingLocalSearch(searchFor);
            var answerResult = JsonConvert.DeserializeObject<Answer>(result.jsonResult);

            if (answerResult.WebPages.Value.Length > 0)
            {
                var answer = answerResult.WebPages.Value[0].Snippet;
                var url = answerResult.WebPages.Value[0].Url;

                chatClient.PostMessage(e.Channel, $"{answer} (Source: {url})");
            }
            else
            {
                chatClient.PostMessage(e.Channel, "I am sorry, but I do not know the answer ...");
            }
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
            string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Create result object for return
            var searchResult = new SearchResult();
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

        private string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            var sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }

    }
}
