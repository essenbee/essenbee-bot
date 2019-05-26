using Essenbee.Bot.Core.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essenbee.Bot.Core.Commands
{
    public class HelpCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Active;
        public string CommandName => "help";
        public string HelpText => "The !help command provides help on the commands available through AlphaBot.";
        public TimeSpan Cooldown { get; }

        private readonly IBot _bot;

        public HelpCommand(IBot bot)
        {
            _bot = bot;
            Cooldown = TimeSpan.FromMinutes(0);
        }

        public Task Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (Status == ItemStatus.Active)
            {
                var helpMsg = string.Empty;

                if (e.ArgsAsList.Count == 0)
                {
                    var allCommands = _bot.CommandHandler.CommandRegistry.Select(x => x.Key).ToList();
                    var sb = new StringBuilder("The following commands are available: ");

                    foreach (var cmd in allCommands)
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
                    if (_bot.CommandHandler.CommandRegistry.TryGetValue(helpForCommand, out ICommand cmd))
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

            return null;
        }

        public bool ShouldExecute() => Status == ItemStatus.Active;
    }
}
