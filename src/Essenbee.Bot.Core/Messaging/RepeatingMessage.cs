using System;
using System.Collections.Generic;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Utilities;

namespace Essenbee.Bot.Core.Messaging
{
    public class RepeatingMessage : IScheduledAction, IMessage, ITimer
    {
        public string Name { get; }
        public string Message { get; }
        public string Channel { get; }
        public TimeSpan Interval { get; }

        public int IntervalInMinutes => Interval.Minutes;
        private DateTime _nextExecutionTime;
        private readonly IClock _clock;
        private readonly IList<IChatClient> _chatClients;
        
        public RepeatingMessage(string channel, string message, int intervalInMinutes,
            IList<IChatClient> chatClients, string name)
        {
            Channel = channel;
            Message = message;
            Interval = TimeSpan.FromMinutes(intervalInMinutes);
            _clock = new SystemClock();
            Name = name;
            _chatClients = chatClients;
            _nextExecutionTime = _clock.UtcNow.AddMinutes(IntervalInMinutes);
        }

        public RepeatingMessage(string channel, string message, int intervalInMinutes, 
            IList<IChatClient> chatClients, IClock clock, string name)
        {
            Channel = channel;
            Message = message;
            Interval = TimeSpan.FromMinutes(intervalInMinutes);
            _clock = clock;
            Name = name;
            _chatClients = chatClients;
            _nextExecutionTime = _clock.UtcNow.AddMinutes(IntervalInMinutes);
        }

        public bool ShouldExecute()
        {
            return _nextExecutionTime <= _clock.UtcNow;
        }

        public void Invoke()
        {

            _nextExecutionTime = _clock.UtcNow.AddMinutes(IntervalInMinutes);

            foreach (var chatClient in _chatClients)
            {
                chatClient.PostMessage(Channel, Message);
            }
        }
    }
}
