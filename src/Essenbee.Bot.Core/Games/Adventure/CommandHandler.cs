using System.Linq;
using Essenbee.Bot.Core.Games.Adventure.Commands;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class CommandHandler
    {
        private readonly AdventureGame _game;
        private readonly AdventureCommandRegistry _commandRegistry;

        public CommandHandler(AdventureGame game)
        {
            _game = game;
            _commandRegistry = new AdventureCommandRegistry(game);
        }

        public void ExecutePlayerCommand(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var advCommands = e.ArgsAsList;
            var cmd = advCommands[0].ToLower();

            var command = _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch(cmd)) ??
                          _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch("use"));

            command?.Invoke(player, e);
        }

        public BaseAdventureCommand GetCommand(string verb) => _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch(verb));
    }
}
