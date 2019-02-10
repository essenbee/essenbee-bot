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
                    if (!CanExecuteNow(chatClient, e, cmd)) return;

                    cmd.Execute(chatClient, e);
                }
            }
        }

        private bool CanExecuteNow(IChatClient chatClient, ChatCommandEventArgs e, ICommand cmd)
        {
            if (cmd.Cooldown > TimeSpan.FromMinutes(0))
            {
                if (e.Role != UserRole.Streamer && e.Role != UserRole.Moderator)
                {
                    if (_bot.CommandInvocations.ContainsKey(e.Command))
                    {
                        var timeInvoked = _bot.CommandInvocations[e.Command];
                        var elapsedTime = DateTimeOffset.Now - timeInvoked;

                        if (elapsedTime >= cmd.Cooldown)
                        {
                            _bot.CommandInvocations.Remove(e.Command);
                        }
                        else
                        {
                            var remaining = (cmd.Cooldown - elapsedTime).Minutes;
                            chatClient.PostMessage(e.Channel,
                                $"The command \"{e.Command}\" is on cooldown, {remaining} minute(s) remaining.");
                            return false;
                        }
                    }
                    else
                    {
                        _bot.CommandInvocations.Add(e.Command, DateTimeOffset.Now);
                    }
                }
            }

            return true;
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
                if (cmd == null) continue;

                cmd.Status = ItemStatus.Active;
                CommandRegistry.Add(cmd.CommandName, cmd);
            }
        }
    }
}
