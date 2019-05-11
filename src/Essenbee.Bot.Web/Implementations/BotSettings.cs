using Essenbee.Bot.Core.Interfaces;
using Microsoft.Extensions.Options;

namespace Essenbee.Bot.Web
{
    public class BotSettings : IBotSettings
    {
        public string AnswerSearchApiKey { get; }
        public IOptions<UserSecrets> Config { get; }

        public BotSettings(IOptions<UserSecrets> config)
        {
            Config = config;
            AnswerSearchApiKey = Config.Value.ProjectAnswerKey;
        }
    }
}
