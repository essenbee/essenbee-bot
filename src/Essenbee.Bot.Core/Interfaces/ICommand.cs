namespace Essenbee.Bot.Core.Interfaces
{
    public interface ICommand
    {
        ItemStatus Status { get; set; }
        string CommandName { get; }
        string HelpText { get; }

        bool ShoudExecute();

        void Execute(IChatClient chatClient, ChatCommandEventArgs e);
    }
}
