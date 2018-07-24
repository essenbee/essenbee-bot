using System.Collections.Generic;
using System.IO;
using System.Linq;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using Hangfire;
using Serilog;

namespace Essenbee.Bot.Web
{
    public class HangfireActionScheduler : IActionScheduler
    {
        public IList<IChatClient> ChatClients { get; }

        public HangfireActionScheduler(IConnectedClients clients)
        {
            ChatClients = clients.ChatClients;
        }

        public void Schedule(IScheduledAction action)
        {
            if (ChatClients is null)
            {
                Log.Error("Chat clients property is not set!");
                throw new InvalidDataException("Chat clients property is not set!"); ;
            }

            Log.Information($"Scheduling {action.Name} with Hangfire server...");

            switch (action)
            {
               case DelayedMessage delayedMsg:
                    var msg = delayedMsg.Message;
                    var chnl = delayedMsg.Channel;

                    foreach (var chatClient in ChatClients)
                    {
                        if (!string.IsNullOrWhiteSpace(chnl))
                        {
                            BackgroundJob.Schedule(() => chatClient.PostMessage(chnl, msg), delayedMsg.Delay);
                        }
                        else
                        {
                            BackgroundJob.Schedule(() => chatClient.PostMessage(msg), delayedMsg.Delay);
                        }
                    }
                    break;

                case RepeatingMessage repeatingMsg:
                    var message = repeatingMsg.Message;
                    var channel = repeatingMsg.Channel;

                    foreach (var chatClient in ChatClients)
                    {
                        if (!string.IsNullOrWhiteSpace(channel))
                        {
                            RecurringJob.AddOrUpdate(
                            repeatingMsg.Name,
                            () => chatClient.PostMessage(channel, message),
                            Cron.MinuteInterval(repeatingMsg.IntervalInMinutes));
                        }
                        else
                        {
                            RecurringJob.AddOrUpdate(
                            repeatingMsg.Name,
                            () => chatClient.PostMessage(message),
                            Cron.MinuteInterval(repeatingMsg.IntervalInMinutes));
                        }
                    }
                    break;
            }
        }

        public List<string> GetRunningJobs<T>()
        {
            var jobs = GetRunningHangfireJobs().Where(o => o.Value.Job.Type == typeof(T));
            return jobs.Select(j => j.Key).ToList();
        }

        public void StopRunningJobs<T>()
        {
            var jobs = GetRunningJobs<T>();
            if (jobs.Any())
            {
                foreach (var job in jobs)
                {
                    BackgroundJob.Delete(job);
                    RecurringJob.RemoveIfExists(job);
                }
            }
        }

        public List<string> GetRunningJobs()
        {
            var jobs = GetRunningHangfireJobs();
            return jobs.Select(j => j.Key).ToList();
        }

        public List<string> GetScheduledJobs()
        {
            var jobs = GetScheduledHangfireJobs();
            return jobs.Select(j => j.Key).ToList();
        }

        public List<string> GetEnqueuedJobs()
        {
            var jobs = GetEnqueuedHangfireJobs();
            return jobs.Select(j => j.Key).ToList();
        }

        private List<KeyValuePair<string, Hangfire.Storage.Monitoring.ProcessingJobDto>> GetRunningHangfireJobs()
        {
            return JobStorage.Current.GetMonitoringApi()
                .ProcessingJobs(0, int.MaxValue).ToList();
        }

        private List<KeyValuePair<string, Hangfire.Storage.Monitoring.ScheduledJobDto>> GetScheduledHangfireJobs()
        {
            return JobStorage.Current.GetMonitoringApi()
                .ScheduledJobs(0, int.MaxValue).ToList();
        }

        private List<KeyValuePair<string, Hangfire.Storage.Monitoring.EnqueuedJobDto>> GetEnqueuedHangfireJobs(string queue = "default")
        {
            return JobStorage.Current.GetMonitoringApi()
                .EnqueuedJobs(queue, 0, int.MaxValue).ToList();
        }
    }
}
