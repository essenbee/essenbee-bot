namespace Essenbee.Bot.Core.Interfaces
{
    public interface IAutoMessaging
    {
        void PublishMessage(IAutoMessage mnessage, ItemStatus status);
        void EnqueueMessagesToDisplay();
        (bool isMessage, string message) DequeueNextMessage();
    }
}
