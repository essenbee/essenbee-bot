using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.Slack;
using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Essenbee.Bot.Web
{
    public class BotWorker : Worker
    {
        private readonly Core.Bot _bot;
        private readonly IOptions<UserSecrets> _config;
        private readonly IRepository _repository;

        public BotWorker(Core.Bot bot, IOptions<UserSecrets> config, IRepository repository)
        {
            _bot = bot;
            _config = config;
            _repository = repository;

            bot.SetRepository(_repository);
            bot.ConnectedClients = ConnectChatClients();
            bot.SetProjectAnswerKey(_config.Value.ProjectAnswerKey);
            bot.SetActionScheduler(new HangfireActionScheduler(bot.ConnectedClients));
        }

        [DisableConcurrentExecution(60)]
        public void Start()
        {
            try
            {
                _bot.Start();
            }
            catch (Exception ex)
            {
                Log.Error($"BotWorker.Start(): {ex.Message} - {ex.StackTrace}");
            }
        }

        private List<IChatClient> ConnectChatClients()
        {
            var slackApiKey = _config.Value.SlackApiKey;
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
