using Essenbee.Bot.Core.Games.Adventure.Commands;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureCommandRegistry
    {
        public IList<BaseAdventureCommand> RegisteredCommands { get; }
        private readonly IReadonlyAdventureGame _game;

        public AdventureCommandRegistry(IReadonlyAdventureGame game)
        {
            _game = game;

            var lookCommand = new Look(_game, "look", "l");
            var helpCommand = new Help(_game, "help");
            var moveCommand = new Move(_game, "move", "go");
            var takeCommand = new Take(_game, "take", "get");
            var inventoryCommand = new Carrying(_game, "inventory", "inv");
            var interactCommand = new Use(_game, "use", "open", "drop", "unlock", "smash", "break");

            RegisteredCommands = new List<BaseAdventureCommand> {
                lookCommand,
                helpCommand,
                moveCommand,
                takeCommand,
                inventoryCommand,
                interactCommand,
            };
        }
    }
}
