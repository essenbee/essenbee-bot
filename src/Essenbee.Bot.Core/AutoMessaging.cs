using System;
using System.Collections.Generic;
using System.Linq;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot.Core
{
    public class AutoMessaging : IAutoMessaging
    {
        public IList<IAutoMessage> PublishedMessages { get; set; } = new List<IAutoMessage>();

        public List<string> MessageQueue { get; set; } = new List<string>();

        private IClock _clock;

        public AutoMessaging(IClock clock)
        {
            _clock = clock;
        }

        public void PublishMessage(IAutoMessage message, ItemStatus status)
        {
            message.Init(DateTime.Now, status);
            PublishedMessages.Add(message);
        }

        public void EnqueueMessagesToDisplay()
        {
            var currentTime = _clock.Now;

            var messages = PublishedMessages.
                Where(msg => msg.ShouldDisplay(currentTime))
                .Select(msg => msg.GetMessage(currentTime));

            MessageQueue.AddRange(messages);
        }

        public (bool isMessage, string message) DequeueNextMessage()
        {
            var msg = string.Empty;

            if (!MessageQueue.Any())
            {
                return (false, msg);
            }

            msg = MessageQueue.First(); // Get the head
            MessageQueue.RemoveAt(0);   // Remove the head

            return (true, msg);
        }
    }
}
