namespace Essenbee.Bot.Core.Interfaces
{
    public interface IGame
    {
        void HandleCommand(IChatClient chatClient, ChatCommandEventArgs e);
    }
}
