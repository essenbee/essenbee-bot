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
        private readonly IBot _bot;
        private readonly IOptions<UserSecrets> _config;

        public BotWorker(IBot bot, IOptions<UserSecrets> config, IRepository repository)
        {
            _bot = bot;
            _config = config;
            bot.SetRepository(repository);
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
