using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IActionScheduler
    {
        IList<IChatClient> ChatClients { get; }

        void Enqueue(Expression<Action> action);
        void Schedule(IScheduledAction action);
        List<string> GetRunningJobs();
        List<string> GetRunningJobs<T>();
        List<string> GetScheduledJobs();
        List<string> GetEnqueuedJobs();
        void StopRunningJobs<T>();
    }
}
