using Essenbee.Bot.Core.Interfaces;
using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.Options;
using Serilog;
using System;

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
            bot.SetProjectAnswerKey(_config.Value.ProjectAnswerKey);
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
    }
}
