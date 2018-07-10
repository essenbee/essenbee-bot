using System.Collections.Generic;
using System.Linq;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Essenbee.Bot.Web.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IConfiguration _config;

        [BindProperty]
        public bool IsRunning { get; set; } = false;

        public AdminModel(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult OnGet()
        {
            var runningJobs = GetRunningJobs();
            IsRunning = runningJobs.Any(j => j.Value.Job.Type == typeof(BotWorker));

            return Page();
        }

        public IActionResult OnPost()
        {
            var runningJobs = GetRunningJobs();
            var botWorkerJobs = runningJobs.Where(o => o.Value.Job.Type == typeof(BotWorker)).ToList();
            var alreadyRunning = runningJobs.Any(j => j.Value.Job.Type == typeof(BotWorker));

            if (botWorkerJobs.Any())
            {
                var jobInstanceIdsToDelete = new List<string>();

                foreach (var botWorkerJob in botWorkerJobs)
                {
                    jobInstanceIdsToDelete.Add(botWorkerJob.Key);
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

            return Page();
        }

        private List<KeyValuePair<string, Hangfire.Storage.Monitoring.ProcessingJobDto>> GetRunningJobs()
        {
            return JobStorage.Current.GetMonitoringApi()
                .ProcessingJobs(0, int.MaxValue).ToList();
        }
    }
}