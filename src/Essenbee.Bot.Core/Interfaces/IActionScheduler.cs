namespace Essenbee.Bot.Core.Interfaces
{
    public interface IActionScheduler
    {
        void Schedule(IScheduledAction action);
    }
}
