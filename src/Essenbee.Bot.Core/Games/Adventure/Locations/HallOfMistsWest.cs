using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class HallOfMistsWest : AdventureLocation
    {
        public HallOfMistsWest(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.HallOfMistsWest;
            Name = "Hall of Mists";
            ShortDescription = "in hall of mists";
            LongDescription = "at one end of a vast hall stretching out of sight to the east.";
            Level = 1;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove("", Location.FissureWest, "north", "n", "e", "east"),
                new PlayerMove("You follow a long winding tunnel...",
                    Location.AllAlike1, "south", "s"),
                //new PlayerMove("",
                //    Location.LongHallEast, "west", "w"),
            };
        }
    }
}
