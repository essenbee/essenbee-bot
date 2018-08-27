using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Debris : AdventureLocation
    {
        public Debris(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Debris;
            Name = "Debris room";
            ShortDescription = "in debris room";
            LongDescription = "in a debris room filled with stuff washed in from the surface. A low wide passage with cobbles becomes plugged with mud and debris" +
                " here, but an awkward canyon leads upward and west.  Scrawled on the wall is a cryptic note: `XYZZY`.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem>();
            ValidMoves = new List<PlayerMove> {
                new PlayerMove(Location.Cobbles, "east", "e"),
            };
        }
    }
}
