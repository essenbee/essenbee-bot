using System;
using System.Collections.Generic;
using System.Linq;
using Essenbee.Bot.Core.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Serilog;

namespace Essenbee.Bot.Web.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IOptions<UserSecrets> _config;
        private readonly IRepository _repository;
        private readonly IActionScheduler _actionScheduler;

        [BindProperty]
        public bool IsRunning { get; set; } = false;
        [BindProperty]
        public IList<Core.Data.TimedMessage> TimedMessages { get; set; }
        [BindProperty]
        public IList<Core.Data.StartupMessage> StartupMessage { get; set; }

        public AdminModel(IOptions<UserSecrets> config, IRepository repository, IActionScheduler actionScheduler)
        {
            _config = config;
            _repository = repository;
            _actionScheduler = actionScheduler;
        }

        public IActionResult OnGet()
        {
            try
            {
                var runningJobs = _actionScheduler.GetRunningJobs<BotWorker>();
                IsRunning = runningJobs.Any();
                TimedMessages = _repository.List<Core.Data.TimedMessage>();
                StartupMessage = _repository.List<Core.Data.StartupMessage>();
            }
            catch (Exception ex)
            {
                Log.Error($"Admin.OnGet(): {ex.Message} - {ex.StackTrace}");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                var runningJobs = _actionScheduler.GetRunningJobs<BotWorker>();
                var botWorkerJobs = runningJobs;
                var alreadyRunning = runningJobs.Any();

                if (botWorkerJobs.Any())
                {
                    var jobInstanceIdsToDelete = new List<string>();

                    foreach (var botWorkerJob in botWorkerJobs)
                    {
                        jobInstanceIdsToDelete.Add(botWorkerJob);
                    }

                    foreach (var id in jobInstanceIdsToDelete)
                    {
                        BackgroundJob.Delete(id);
                        RecurringJob.RemoveIfExists(id);
                    }

                    IsRunning = false;
                }
                else
                {
                    BackgroundJob.Enqueue<BotWorker>(bw => bw.Start());
                    IsRunning = true;
                }

                TimedMessages = _repository.List<Core.Data.TimedMessage>();
                StartupMessage = _repository.List<Core.Data.StartupMessage>();
            }
            catch (Exception ex)
            {
                Log.Error($"Admin.OnPost(): {ex.Message} - {ex.StackTrace}");
            }

            return Page();
        }
    }
}