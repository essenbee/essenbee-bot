using System;
using System.Collections.Generic;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot
{
    public class ConsoleChatClient : IChatClient
    {
        public event EventHandler<Core.ChatCommandEventArgs> OnChatCommandReceived = null;

        public IDictionary<string, string> Channels
        {
            get => new Dictionary<string, string> { {"console", "0"} };
            set => throw new NotSupportedException("You cannot set Channels on the ConsoleChatClient");
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnecting from the Console service ...");
        }

        public void PostMessage(string channel, string text)
        {
            Console.WriteLine(text);
        }

        public void PostMessage(string text)
        {
            Console.WriteLine(text);
        }
    }
}
