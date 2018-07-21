using System.Collections.Generic;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IActionScheduler
    {
        IList<IChatClient> ChatClients { get; set; }

        void Schedule(IScheduledAction action);
        List<string> GetRunningJobs();
        List<string> GetRunningJobs<T>();
        List<string> GetScheduledJobs();
        List<string> GetEnqueuedJobs();
    }
}
