using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class TrollBridge : AdventureLocation
    {
        public TrollBridge(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.TrollBridge;
            Name = "Troll Bridge";
            ShortDescription = "standing on a rickety wooden bridge";
            LongDescription = "standing on a rickety wooden bridge that spans a wide and deep chasm.";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                    new PlayerMove(string.Empty, Location.NorthEastOfChasm, "northeast", "ne"),
                    new PlayerMove(string.Empty, Location.SouthWestOfChasm, "southwest", "sw"),
                };
        }
    }
}