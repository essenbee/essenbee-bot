using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class LongHallEast : AdventureLocation
    {
        public LongHallEast(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.LongHallEast;
            Name = "long hall east";
            ShortDescription = "at the East end of long hall";
            LongDescription = "at the East end of a very long hall apparently without side " +
                "chambers. To the East a low wide crawl slants up. To the North a " +
                "round two foot hole slants down.";
            Level = 1;
            Items = new List<IAdventureItem>();

            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.Crossover, "north", "n", "down", "d"),
                new PlayerMove(string.Empty, Location.LongHallWest, "west", "w"),
                new PlayerMove(string.Empty, Location.HallOfMistsWest, "east", "e", "up", "u"),
            };
        }
    }
}
