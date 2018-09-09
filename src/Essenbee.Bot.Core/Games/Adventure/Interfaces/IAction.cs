namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IAction
    {
        bool Do(IAdventurePlayer player, IAdventureItem item);
    }
}
