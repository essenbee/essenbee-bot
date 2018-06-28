namespace Essenbee.Bot.Core.Interfaces
{
    public interface ITimedAction
    {
        string Name { get; }
        bool ShouldRun();
        void Invoke();
    }
}
