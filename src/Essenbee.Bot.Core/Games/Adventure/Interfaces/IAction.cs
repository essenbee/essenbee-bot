namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IAction
    {
        bool Do(AdventurePlayer player, AdventureItem item);
    }
}
