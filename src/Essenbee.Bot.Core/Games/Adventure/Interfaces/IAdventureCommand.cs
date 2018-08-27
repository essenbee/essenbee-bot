namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IAdventureCommand
    {
        void Invoke(AdventurePlayer player, ChatCommandEventArgs e);
        bool IsMatch(string verb);
    }
}
