using System.Collections.Generic;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IChatClient
    {
        IDictionary<string, string> Channels { get; set; }
        void PostMessage(string channel, string text);
    }
}
