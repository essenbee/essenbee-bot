using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class RemoveDestination : IAction
    {
        private Location _destination;
        private readonly IReadonlyAdventureGame _game;

        public RemoveDestination(IReadonlyAdventureGame game, Location destination)
        {
            _game = game;
            _destination = destination;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            player.CurrentLocation.RemoveDestination(_destination);
            return true;
        }
    }
}
