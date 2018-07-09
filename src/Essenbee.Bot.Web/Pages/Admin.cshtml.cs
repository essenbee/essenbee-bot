using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.Slack;
using Hangfire;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Essenbee.Bot.Web.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IConfiguration _config;

        public AdminModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            var runningJobs = JobStorage.Current.GetMonitoringApi()
                .ProcessingJobs(0, int.MaxValue).ToList();

            var alreadyRunning = runningJobs.Any(j => j.Value.Job.Type == typeof(BotWorker));
            if (!alreadyRunning)
            { 
                BackgroundJob.Enqueue<BotWorker>(bw => bw.Start());
            }
        }
    }
}