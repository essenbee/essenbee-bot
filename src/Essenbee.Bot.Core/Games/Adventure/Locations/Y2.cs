using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Y2 : AdventureLocation
    {
        public Y2(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Y2;
            Name = "Y2";
            ShortDescription = "at Y2";
            LongDescription = "in a large room, with a passage to the south, a passage to the west, and a wall of broken rock to the east." + 
                "There is a large `Y2` on a rock in the room's centre.";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.LowPassage, "south", "s"),
                new PlayerMove(string.Empty, Location.JumbleOfRocks, "east", "e"),
                new PlayerMove(string.Empty, Location.Window1, "west", "w"),
            };
        }
    }
}