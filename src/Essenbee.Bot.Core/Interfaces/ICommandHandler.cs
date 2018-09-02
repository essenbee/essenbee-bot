using System.Collections.Generic;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface ICommandHandler
    {
        Dictionary<string, ICommand> CommandRegistry { get; }

        void ExecuteCommand(IChatClient chatClient, ChatCommandEventArgs e);
    }
}