namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
{
    public interface IAdventureCommand
    {
        void Invoke(IAdventurePlayer player, ChatCommandEventArgs e);
        bool IsMatch(string verb);
        bool CheckEvents { get; set; }
    }
}
