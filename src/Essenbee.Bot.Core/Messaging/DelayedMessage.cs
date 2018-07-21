using System;
using System.Collections.Generic;
using System.Text;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot.Core.Messaging
{
    public class DelayedMessage : IScheduledAction, IMessage, IDelayed
    {
        public string Name { get; }
        public string Message { get; }
        public string Channel { get; }
        public TimeSpan Delay { get; }

        public int DelayInSeconds => Delay.Seconds;
        private DateTime _nextExecutionTime;
        private readonly IList<IChatClient> _chatClients;

        public DelayedMessage(string channel, string message, int delayInSeconds,
            IList<IChatClient> chatClients, string name)
        {
            Channel = channel;
            Message = message;
            Delay = TimeSpan.FromSeconds(delayInSeconds);
            Name = name;
            _chatClients = chatClients;
        }

        public bool ShouldExecute() => DateTime.Now > _nextExecutionTime;
        
        public void Invoke()
        {
            _nextExecutionTime = DateTime.MaxValue;

            foreach (var chatClient in _chatClients)
            {
                chatClient.PostMessage(Channel, Message);
            }
        }
    }
}
