using System;
using System.Collections.Generic;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot
{
    public class ConsoleChatClient : IChatClient
    {
        public IDictionary<string, string> Channels
        {
            get => new Dictionary<string, string> { {"console", "0"} };
            set => throw new NotSupportedException("You cannot set Channels on the ConsoleChatClient");
        } 

        public void PostMessage(string channel, string text)
        {
            Console.WriteLine(text);
        }
    }
}
