using Essenbee.Bot.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBot _bot;
        private readonly IRepository _repository;
        private readonly IActionScheduler _actionScheduler;

        [BindProperty]
        public bool IsRunning { get; set; } = false;
        [BindProperty]
        public IList<Core.Data.TimedMessage> TimedMessages { get; set; }
        [BindProperty]
        public IList<Core.Data.StartupMessage> StartupMessage { get; set; }

        public IndexModel(IBot bot, IRepository repository, IActionScheduler actionScheduler)
        {
            _bot = bot;
            _repository = repository;
            _actionScheduler = actionScheduler;
        }

        public IActionResult OnGet()
        {
            try
            {
                var runningJobs = _actionScheduler.GetRunningJobs<Core.Bot>();
                IsRunning = runningJobs.Any();
                TimedMessages = _repository.List<Core.Data.TimedMessage>();
                StartupMessage = _repository.List<Core.Data.StartupMessage>();
            }
            catch (Exception ex)
            {
                Log.Error($"Index.OnGet(): {ex.Message} - {ex.StackTrace}");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                var runningJobs = _actionScheduler.GetRunningJobs<Core.Bot>();
                StartupMessage = _repository.List<Core.Data.StartupMessage>();
                var startUpMessage = StartupMessage.FirstOrDefault();

                if (runningJobs.Any())
                {
                    _bot.Stop("AlphaBot is offline."); // ToDo: add to database
                    _actionScheduler.StopRunningJobs<Core.Bot>();
                    IsRunning = false;
                }
                else
                {
                    _actionScheduler.Enqueue(() => _bot.Start(startUpMessage.Message));
                    _bot.ScheduleRepeatedMessages(_actionScheduler, _repository);
                    IsRunning = true;
                }

                TimedMessages = _repository.List<Core.Data.TimedMessage>();
            }
            catch (Exception ex)
            {
                Log.Error($"Index.OnPost(): {ex.Message} - {ex.StackTrace}");
            }

            return Page();
        }
    }
}
