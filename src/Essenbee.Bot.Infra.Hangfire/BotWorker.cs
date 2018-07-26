using Essenbee.Bot.Core.Interfaces;
using Hangfire;
using Hangfire.Server;
using System;
using static System.Console;

namespace Essenbee.Bot.Infra.Hangfire
{
    public class BotWorker : Worker
    {
        private readonly IBot _bot;
        private readonly IRepository _repository;

        public BotWorker(IBot bot, IRepository repository)
        {
            _bot = bot;
            _repository = repository;
            bot.SetRepository(_repository);
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
                WriteLine($"BotWorker.Start(): {ex.Message} - {ex.StackTrace}");
            }
        }
    }
}
