namespace Essenbee.Bot.Core.Interfaces
{
    public interface IScheduledAction
    {
        string Name { get; }
        bool ShouldExecute();
        void Invoke();
    }
}
