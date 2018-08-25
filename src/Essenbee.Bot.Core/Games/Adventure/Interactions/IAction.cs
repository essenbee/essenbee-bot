namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public interface IAction
    {
        bool Do(AdventurePlayer player, AdventureItem item);
    }
}
