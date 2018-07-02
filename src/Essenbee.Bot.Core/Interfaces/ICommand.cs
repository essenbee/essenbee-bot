namespace Essenbee.Bot.Core.Interfaces
{
    public interface ICommand
    {
        ItemStatus Status { get; set; }
        string CommandName { get; set; }
        string HelpText { get; set; }

        bool ShoudExecute();

        void Execute(ChatCommandEventArgs e);
    }
}
