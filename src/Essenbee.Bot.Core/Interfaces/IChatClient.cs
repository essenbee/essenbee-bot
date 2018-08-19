using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IChatClient
    {
        event EventHandler<ChatCommandEventArgs> OnChatCommandReceived;

        IDictionary<string, string> Channels { get; set; }

        void PostMessage(string channel, string text);

        void PostMessage(string text);

        void PostDirectMessage(string username, string text);

        void Disconnect();
    }
}
