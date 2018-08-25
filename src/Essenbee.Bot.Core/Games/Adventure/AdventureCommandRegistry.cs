using Essenbee.Bot.Core.Games.Adventure.Commands;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureCommandRegistry
    {
        public IDictionary<string, IAdventureCommand> RegisteredCommands { get; }
        private readonly IReadonlyAdventureGame _game;

        public AdventureCommandRegistry(IReadonlyAdventureGame game)
        {
            _game = game;

            var lookCommand = new Look(_game);
            var helpCommand = new Help(_game);
            var moveCommand = new Move(_game);
            var takeCommand = new Take(_game);
            var inventoryCommand = new Carrying(_game);
            var dropCommand = new Drop(_game);
            var useCommand = new Use(_game);

            RegisteredCommands = new Dictionary<string, IAdventureCommand> {
                { "look", lookCommand },
                { "l", lookCommand },
                { "help", helpCommand },
                { "go", moveCommand },
                { "move", moveCommand },
                { "take", takeCommand },
                { "get", takeCommand },
                { "drop", dropCommand },
                { "inventory", inventoryCommand },
                { "inv", inventoryCommand },
                { "use", useCommand },
                { "read", useCommand },
                { "smash", useCommand },
                { "break", useCommand },
            };
        }
    }
}
