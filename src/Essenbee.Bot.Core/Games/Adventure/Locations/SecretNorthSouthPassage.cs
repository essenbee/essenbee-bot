using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SecretNorthSouthPassage : AdventureLocation
    {
        public SecretNorthSouthPassage(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SecretNorthSouthPassage;
            Name = "secret North-South passage";
            ShortDescription = "in a secret North/South canyon above a sizable passage.";
            LongDescription = "in a secret North/South canyon above a sizable passage.";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.SecretJunction, "north", "n"),
                new PlayerMove(string.Empty, Location.StalactiteRoom, "south", "s"),
            };
        }
    }
}
