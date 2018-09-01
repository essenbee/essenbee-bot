using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Canyon : AdventureLocation
    {
        public Canyon(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Canyon;
            Name = "Awkward Canyon";
            ShortDescription = "in awkward canyon";
            LongDescription = "in an awkward sloping east/west canyon.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You struggle through several tight spots...", Location.Debris, "east", "e", "down", "d"),
                new PlayerMove("You have to contort your body in order to make your way forward...", Location.BirdChamber, "west", "w"),
            };
        }
    }
}
