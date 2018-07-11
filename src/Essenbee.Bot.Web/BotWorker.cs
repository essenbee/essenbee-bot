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
        private readonly IRepository _repository;

        public BotWorker(Core.Bot bot, IConfiguration config, IRepository repository)
        {
            _bot = bot;
            _config = config;
            _repository = repository;

            bot.SetRepository(_repository);
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

            Thread.Sleep(2000);

            return connectedClients;
        }
    }
}
