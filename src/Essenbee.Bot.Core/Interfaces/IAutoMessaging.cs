using System;
using System.Collections.Generic;
using System.Text;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IAutoMessaging
    {
        void PublishMessage(IAutoMessage mnessage);
        void EnqueueMessagesToDisplay();
        (bool isMessage, string message) DequeueNextMessage();
    }
}
