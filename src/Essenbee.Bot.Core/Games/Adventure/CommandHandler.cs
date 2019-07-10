using System.Linq;
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

            command?.Invoke(player, e);

            return command.CheckEvents;
        }

        public IAdventureCommand GetCommand(string verb) => _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch(verb));
    }
}
