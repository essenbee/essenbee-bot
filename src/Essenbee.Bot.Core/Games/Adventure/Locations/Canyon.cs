using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Canyon : AdventureLocation
    {
        public Canyon(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Cobbles;
            Name = "Awkward Canyon";
            ShortDescription = "in awkward canyon";
            LongDescription = "in an awkward sloping east/west canyon.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem>();
            ValidMoves = new List<PlayerMove> {
                new PlayerMove(Location.Debris, "east", "e", "down", "d"),
            };
        }
    }
}
