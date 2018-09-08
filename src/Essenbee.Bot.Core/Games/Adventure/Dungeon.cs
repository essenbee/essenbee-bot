using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class Dungeon : IDungeon
    {
        private readonly Dictionary<Location, IAdventureLocation> _locations;

        public Dungeon(AdventureGame game)
        {
            _locations = new ColossalCave().Build(game);
        }

        public bool TryGetLocation(Location locationId, out IAdventureLocation place)
        {
            var location = _locations.Where(l => l.Value.LocationId.Equals(locationId)).ToList();
            place = null;

            if (location.Count == 0)
            {
                return false;
            }

            place = location[0].Value;

            return true;
        }

        public IAdventureLocation GetStartingLocation()
        {
            var startingLocations = _locations.Where(l => l.Value.IsStart);
            return startingLocations.First().Value;
        }
    }
}
