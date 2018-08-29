using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Forest : AdventureLocation
    {
        public Forest(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Forest;
            Name = "Forest";
            ShortDescription = "in open forest";
            LongDescription = "in open forest, with a deep valley to one side.";
            Items = new List<AdventureItem>();
            ValidMoves = new List<PlayerMove> {
                new PlayerMove(string.Empty, Location.Valley, "valley", "east", "east", "down", "d"),
                new PlayerMove(string.Empty, Location.Forest, "west", "w", "south", "s"),
            };
            // ToDo: if forest, north or forward, 50% -> forest, 50% - forest2
        }
    }
}
