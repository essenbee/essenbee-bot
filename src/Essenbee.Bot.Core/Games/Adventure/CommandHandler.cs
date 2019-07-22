using System.Linq;
using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class CommandHandler : IAdventureCommandHandler
    {
        private readonly AdventureGame _game;
        private readonly AdventureCommandRegistry _commandRegistry;

        public CommandHandler(AdventureGame game)
        {
            _game = game;
            _commandRegistry = new AdventureCommandRegistry(game);
        }

        public bool ExecutePlayerCommand(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var advCommands = e.ArgsAsList;
            var cmd = advCommands[0].ToLower();

            var command = _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch(cmd)) ??
                          _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch("use"));

            if (!command.IsVerbatim)
            {
                var nullWords = new List<string> { "a", "an", "the", "this", "that", "to" };
                var argsSent = advCommands.ToArray();

                foreach (var word in argsSent.Where(word => nullWords.Contains(word)).Select(word => word))
                {
                    advCommands.Remove(word);
                }
            }

            command?.Invoke(player, e);

            return command.CheckEvents;
        }

        public IAdventureCommand GetCommand(string verb) => _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch(verb));
    }
}
