using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class RemoveDestination : IAction
    {
        private Location _destination;
        private Location _from = Location.Nowhere;
        private readonly IReadonlyAdventureGame _game;

        public RemoveDestination(IReadonlyAdventureGame game, Location destination)
        {
            _game = game;
            _destination = destination;
        }

        public RemoveDestination(IReadonlyAdventureGame game, Location destination, Location from)
        {
            _game = game;
            _destination = destination;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            if (_from == Location.Nowhere)
            {
                player.CurrentLocation.RemoveDestination(_destination);
            }
            else
            {
                if (!_game.Dungeon.TryGetLocation(_from, out var fromLocation))
                {
                    return false;
                }

                fromLocation.RemoveDestination(_destination);
            }

            return true;
        }
    }
}
