using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public partial class SouthWestOfChasm : AdventureLocation
    {
        public SouthWestOfChasm(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SouthWestOfChasm;
            Name = "South West Side of a Chasm";
            ShortDescription = "on the southwest side of a chasm";
            LongDescription = "on one side of a large, deep chasm. A heavy white mist rising " +
                              "up from below obscures all view of the far side.  A SW path leads away " +
                              "from the chasm into a winding corridor. ";
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Troll) };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.LongWindingCorridor, "southwest", "sw", "down", "d"),
            };
        }
    }
}
