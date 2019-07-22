using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class Dungeon : IDungeon
    {
        public Dictionary<Location, IAdventureLocation> Locations { get; }

        public Dungeon(AdventureGame game, IDungeonBuilder builder)
        {
            Locations = builder.Build(game);
        }

        public bool TryGetLocation(Location locationId, out IAdventureLocation place)
        {
            var location = Locations.Where(l => l.Value.LocationId.Equals(locationId)).ToList();
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
            var startingLocations = Locations.Where(l => l.Value.IsStart);
            return startingLocations.First().Value;
        }
    }
}
