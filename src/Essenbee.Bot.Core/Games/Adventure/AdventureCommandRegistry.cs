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
            var moveCommand = new Move(_game, "move", "go", "walk", "run");
            var takeCommand = new Take(_game, "take", "get", "grab");
            var inventoryCommand = new Carrying(_game, "inventory", "inv");
            var interactCommand = new Interact(_game, "use"); // Do not add aliases for this command

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
