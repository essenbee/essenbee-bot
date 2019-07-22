using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class TightNorthSouthCanyon : AdventureLocation
    {
        public TightNorthSouthCanyon(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.TightNorthSouthCanyon;
            Name = "tight canyon";
            ShortDescription = "in a tight canyon which exits to the north and south";
            LongDescription = "in a tight canyon which exits to the north and south. " +
                "There is a crack in the roof, but there is no way you could climb up there...";
            Level = 1;
            Items = new List<IAdventureItem>();

            ValidMoves = new List<IPlayerMove> {

            };
        }
    }
}
