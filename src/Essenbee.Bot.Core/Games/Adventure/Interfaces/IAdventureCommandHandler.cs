namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IAdventureCommandHandler
    {
        bool ExecutePlayerCommand(AdventurePlayer player, ChatCommandEventArgs e);
        IAdventureCommand GetCommand(string verb);
    }
}
