using System;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot
{
    public class ConsoleChatClient : IChatClient
    {
        public void PostMessage(string channel, string text)
        {
            Console.WriteLine(text);
        }
    }
}
