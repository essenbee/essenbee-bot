﻿using Essenbee.Bot.Core.Interfaces;
using Hangfire;
using Hangfire.Server;
using Serilog;
using System;

namespace Essenbee.Bot.Web
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
                Log.Error($"BotWorker.Start(): {ex.Message} - {ex.StackTrace}");
            }
        }
    }
}
