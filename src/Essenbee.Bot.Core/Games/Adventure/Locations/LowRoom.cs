using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class LowRoom : AdventureLocation
    {
        public LowRoom(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.LowRoom;
            Name = "large low room";
            ShortDescription = "in a large low room. Crawls lead north, SE, and SW.";
            LongDescription = "in a large low room. Crawls lead north, SE, and SW.";
            Level = 1;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>
            {
                //new PlayerMove(string.Empty, Location., "north", "n"),      // dead end
                //new PlayerMove(string.Empty, Location., "southwest", "sw"), // long winding corridor
                //new PlayerMove(string.Empty, Location., "southeast", "se"), // oriental room
                new PlayerMove(string.Empty, Location.Bedquilt, "bedquilt"),
            };
        }
    }
}
