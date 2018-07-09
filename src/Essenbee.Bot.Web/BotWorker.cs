using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.Slack;
using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;

namespace Essenbee.Bot.Web
{
    public class BotWorker : Worker
    {
        private readonly Core.Bot _bot;
        private readonly IConfiguration _config;

        public BotWorker(Core.Bot bot, IConfiguration config)
        {
            _bot = bot;
            _config = config;
            bot.ConnectedClients = ConnectChatClients();
            bot.SetProjectAnswerKey(_config["UserSecrets:ProjectAnswerKey"]);
        }

        [DisableConcurrentExecution(60)]
        public void Start()
        {
            _bot.Start();
        }

        private List<IChatClient> ConnectChatClients()
        {
            var slackApiKey = _config["UserSecrets:SlackApiKey"];
            var connectedClients = new List<IChatClient>
            {
                new ConsoleChatClient(),
                new SlackChatClient(slackApiKey),
            };

            Thread.Sleep(5000);

            return connectedClients;
        }
    }
}
