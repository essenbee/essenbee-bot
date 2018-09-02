namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IAdventureCommandHandler
    {
        void ExecutePlayerCommand(AdventurePlayer player, ChatCommandEventArgs e);
        IAdventureCommand GetCommand(string verb);
    }
}
