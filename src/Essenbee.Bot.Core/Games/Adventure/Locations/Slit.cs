using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Slit : AdventureLocation
    {
        public Slit(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Slit;
            Name = "Slit";
            ShortDescription = "at the slit in the streambed";
            LongDescription = "besides the stream. At your feet all the water of the stream splashes into a 2-inch slit in the rock. Downstream the streambed is bare rock.";
            WaterPresent = true;
            Items = new List<AdventureItem>();
            ValidMoves = new List<PlayerMove> {
                new PlayerMove(Location.Valley, "valley", "north", "n", "upstream"),
                new PlayerMove(Location.Depression, "downstream", "south", "s"),
                new PlayerMove(Location.Forest, "forest", "east", "west", "e", "w"),
            };
        }
    }
}
