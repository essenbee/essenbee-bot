namespace Essenbee.Bot.Core.Data
{
    public class TimedMessage : DataEntity
    {
        public TimedMessage()
        {
        }

        public TimedMessage(int delay, string message, ItemStatus status)
        {
            Delay = delay;
            Message = message;
            Status = status;
        }

        public int Delay { get; protected set; }
        public string Message { get; protected set; }
        public ItemStatus Status { get; protected set; }
    }
}
