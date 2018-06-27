namespace Essenbee.Bot.Core.Interfaces
{
    public interface IIntervalAction
    {
        string Name { get; }
        bool ShouldRun();
        void Invoke();
    }
}
