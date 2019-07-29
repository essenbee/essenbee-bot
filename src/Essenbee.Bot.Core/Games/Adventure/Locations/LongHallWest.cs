using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class LongHallWest : AdventureLocation
    {
        public LongHallWest(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.LongHallWest;
            Name = "long hall west";
            ShortDescription = "at the West end of long hall";
            LongDescription = "at the West end of a very long featureless hall. The hall " +
                "joins up with a narrow North/South passage.";
            Level = 1;
            IsDark = true;
            Items = new List<IAdventureItem>();

            ValidMoves = new List<IPlayerMove>
            {
                //new PlayerMove(string.Empty, Location., "north", "n"), // To Maze all different entrance
                new PlayerMove(string.Empty, Location.Crossover, "south", "s"),
                new PlayerMove(string.Empty, Location.LongHallEast, "east", "e"),
            };
        }
    }
}
