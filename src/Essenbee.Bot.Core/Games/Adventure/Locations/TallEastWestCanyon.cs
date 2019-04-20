using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class TallEastWestCanyon : AdventureLocation
    {
        public TallEastWestCanyon(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.TallEastWestCanyon;
            Name = "secret North-South passage";
            ShortDescription = "in a tall East-West canyon.";
            LongDescription = "in a tall East-West canyon. A low tight crawl goes 3 feet North and then seems to open up.";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("With difficulty, you manage to squeeze yourself through the constriction...",
                    Location.SwissCheese, "north", "n"),
                new PlayerMove(string.Empty, Location.TightNorthSouthCanyon, "east", "e"),
                //new PlayerMove(string.Empty, Location., "west", "w"),
            };
        }
    }
}
