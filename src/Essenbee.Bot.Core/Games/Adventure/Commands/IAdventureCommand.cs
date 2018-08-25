namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public interface IAdventureCommand
    {
        void Invoke(AdventurePlayer player, ChatCommandEventArgs e);
        bool IsMatch(string verb);
    }
}
