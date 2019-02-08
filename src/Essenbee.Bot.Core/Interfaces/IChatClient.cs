using System;
using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IChatClient
    {
        event EventHandler<ChatCommandEventArgs> OnChatCommandReceived;

        bool UseUsernameForIM { get; }
        string DefaultChannel { get; }
        IDictionary<string, string> Channels { get; set; }

        void PostMessage(string channel, string text);
        void PostMessage(string text);
        void PostDirectMessage(string username, string text);
        void PostDirectMessage(IAdventurePlayer player, string text);
        void Disconnect();
    }
}
