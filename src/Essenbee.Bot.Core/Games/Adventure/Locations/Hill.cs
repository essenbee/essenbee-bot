using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Hill : AdventureLocation
    {
        public Hill(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Hill;
            Name = "Hill";
            ShortDescription = "atop a hill";
            LongDescription = "atop hill, still in the forest. The road slopes back down the other side of the hill.There is a building in the distance.";
            Items = new List<AdventureItem>();
            ValidMoves = new List<PlayerMove> {
                new PlayerMove(Location.Forest, "forest", "south", "s", "north", "n"),
                new PlayerMove(Location.Road, "road", "e", "east", "forward"),
            };
        }
    }
}
