using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SecretJunction : AdventureLocation
    {
        public SecretJunction(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SecretJunction;
            Name = "secret junction";
            ShortDescription = "at a junction of three secret canyons";
            LongDescription = "in a secret canyon at a junction of three canyons, bearing North, " +
                "South, and SE.  The North one is as tall as the other two combined.";
            Items = new List<IAdventureItem>();
            IsDark = true;
            Level = 1;
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.SecretNorthSouthPassage, "south", "s"),
                new PlayerMove(string.Empty, Location.Window2, "north", "n"),
                new PlayerMove(string.Empty, Location.Bedquilt, "southeast", "se"),
            };
        }
    }
}
