using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class FissureEast : AdventureLocation
    {
        public FissureEast(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.FissureEast;
            Name = "East Bank of Fissure";
            ShortDescription = "on the edge of a deep fissure";
            LongDescription = "standing on the eastern side of a wide, impassible fissure in the rock.";
            IsDark = true;
            Level = 1;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.HallOfMistsEast, "east", "e"),
            };
        }
    }
}
