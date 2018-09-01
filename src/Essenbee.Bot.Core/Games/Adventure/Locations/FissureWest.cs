using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class FissureWest : AdventureLocation
    {
        public FissureWest(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.FissureWest;
            Name = "West Bank of Fissure";
            ShortDescription = "on the edge of a deep fissure";
            LongDescription = "standing on the western side of a wide fissure in the rock.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> ();
        }
    }
}
