using Essenbee.Bot.Core.Interfaces;
using System.Linq;
using System.Text;

namespace Essenbee.Bot.Core.Commands
{
    public class HelpCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Active;
        public string CommandName => "help";
        public string HelpText => "The !help command provides help on the commands available through CoreBot.";

        private readonly IBot _bot;

        public HelpCommand(IBot bot)
        {
            _bot = bot;
        }

        public void Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (Status != ItemStatus.Active) return;

            var helpMsg = string.Empty;

            if (e.ArgsAsList.Count == 0)
            {
                var allCommands = Bot._CommandsAvailable.Select(x => x.Key).ToList();
                var sb = new StringBuilder("The following commands are available: ");

                foreach(var cmd in allCommands)
                {
                    sb.Append($"!{cmd}, ");
                }

                helpMsg = sb.ToString().Trim();
                helpMsg = helpMsg.Remove(helpMsg.Length - 1, 1);
            }
            else
            {
                var helpForCommand = e.ArgsAsList[0];
                // Check command name against available commands ...
                if (Bot._CommandsAvailable.TryGetValue(helpForCommand, out ICommand cmd))
                {
                    helpMsg = cmd.HelpText;
                }
                else
                {
                    helpMsg = $"The command {helpForCommand} has not been implemented.";
                }
            }

            chatClient.PostMessage(e.Channel, helpMsg);
        }

        public bool ShoudExecute()
        {
            return Status == ItemStatus.Active;
        }
    }
}
