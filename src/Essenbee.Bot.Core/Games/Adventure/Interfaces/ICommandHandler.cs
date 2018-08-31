namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface ICommandHandler
    {
        void ExecutePlayerCommand(AdventurePlayer player, ChatCommandEventArgs e);
        IAdventureCommand GetCommand(string verb);
    }
}
