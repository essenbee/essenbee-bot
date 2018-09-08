using Essenbee.Bot.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Commands
{
    public class BotCommandHandler : ICommandHandler
    {
        private readonly IBot _bot;

        public Dictionary<string, ICommand> CommandRegistry { get; }

        public BotCommandHandler(IBot bot)
        {
            CommandRegistry = new Dictionary<string, ICommand>();
            _bot = bot;
            LoadCommands();
        }

        public void ExecuteCommand(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (Bot.IsRunning)
            {
                // Check command name against available commands ...
                if (CommandRegistry.TryGetValue(e.Command, out ICommand cmd))
                {
                    cmd.Execute(chatClient, e);
                }
                else
                {
                    chatClient.PostMessage(e.Channel, $"The command {e.Command} has not been implemented.");
                }
            }
        }

        private void LoadCommands()
        {
            if (CommandRegistry.Count > 0)
            {
                return;
            }

            var commandTypes = GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICommand)));

            foreach (var type in commandTypes)
            {
                if (type.Name == "ICommand") continue;

                var cmd = Activator.CreateInstance(type, _bot) as ICommand;
                cmd.Status = ItemStatus.Active;
                CommandRegistry.Add(cmd.CommandName, cmd);
            }
        }
    }
}
