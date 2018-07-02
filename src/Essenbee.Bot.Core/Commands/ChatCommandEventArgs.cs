using System.Collections.Generic;

namespace Essenbee.Bot.Core
{
    public class ChatCommandEventArgs
    {
        public string Command { get; }
        public List<string> ArgsAsList { get; }
        public string Channel { get; }
        public string UserName { get; }

        public ChatCommandEventArgs(string command, List<string> args, string channel, string userName)
        {
            Channel = channel;
            Command = command;
            ArgsAsList = args;
            UserName = userName;
        }
    }
}
