using System.Collections.Generic;
using System.Net;

namespace Essenbee.Bot.Infra.CognitiveServices
{
    public struct SearchResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string RetryAfter { get; set; }
        public string JsonResult { get; set; }
        public Dictionary<string, string> RelevantHeaders { get; set; }
    }
}
