using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    class Bedquilt : AdventureLocation
    {
        public Bedquilt(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.TightNorthSouthCanyon;
            Name = "tight canyon";
            ShortDescription = "in a tight canyon which exits to the north and south";
            LongDescription = "in a tight canyon which exits to the north and south. " +
                "There is a crack in the roof, but there is no way you could climb up there...";

            Items = new List<IAdventureItem>();

            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.SlabRoom, "slab"),
                //new PlayerMove(string.Empty, Location., "west", "w"), // swiss cheese
                //new PlayerMove(string.Empty, Location., "east", "e"), // complex junction
                // complex random movement for n,s,up or down 
            };
        }
    }
}
