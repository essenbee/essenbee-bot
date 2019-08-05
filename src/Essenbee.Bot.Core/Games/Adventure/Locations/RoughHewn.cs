using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class RoughHewn : AdventureLocation
    {
        public RoughHewn(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.RoughHewn;
            Name = "Rough Hewn Cave";
            ShortDescription = "in a rough hewn cave";
            LongDescription = "in a rough hewn cave";
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.VendingMachine, "north", "n"),
            };
        }
    }
}
