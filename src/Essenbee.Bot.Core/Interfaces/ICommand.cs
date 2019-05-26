using System;
using System.Threading.Tasks;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface ICommand
    {
        ItemStatus Status { get; set; }
        string CommandName { get; }
        string HelpText { get; }
        TimeSpan Cooldown { get; }

        bool ShouldExecute();

        Task Execute(IChatClient chatClient, ChatCommandEventArgs e);
    }
}
