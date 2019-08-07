using Essenbee.Bot.Core.Games.Adventure.Commands;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureCommandRegistry
    {
        public IList<IAdventureCommand> RegisteredCommands { get; }

        public AdventureCommandRegistry(IReadonlyAdventureGame game)
        {
            var lookCommand = new Look(game, "look", "l");
            var helpCommand = new Help(game, "help");
            var moveCommand = new Move(game, "move", "go", "walk", "run", "climb", "crawl", "head");
            var takeCommand = new Take(game, "take", "get", "grab");
            var dropCommand = new Drop(game, "drop", "discard");
            var magicWord1 = new Xyzzy(game, "xyzzy");
            var magicWord2 = new Plugh(game, "plugh");
            var inventoryCommand = new Carrying(game, "inventory", "inv");
            var interactCommand = new Interact(game, "use"); // Do not add aliases for this command
            var yes = new Yes(game, "yes", "y");
            var no = new No(game, "no", "n");
            var say = new Say(game, "say");
            var back = new Back(game, "back", "retreat");

            // ToDo: Remove this as its for TESTING ONLY
            var teleport = new Teleport(game, "teleport");

            RegisteredCommands = new List<IAdventureCommand> {
                lookCommand,
                helpCommand,
                moveCommand,
                takeCommand,
                dropCommand,
                magicWord1,
                magicWord2,
                inventoryCommand,
                interactCommand,
                yes,
                no,
                say,
                back,
                teleport,
            };
        }
    }
}
