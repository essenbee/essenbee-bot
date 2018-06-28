namespace Essenbee.Bot.Core.Interfaces
{
    public interface IChatClient
    {
        void PostMessage(string channel, string text);
    }
}
