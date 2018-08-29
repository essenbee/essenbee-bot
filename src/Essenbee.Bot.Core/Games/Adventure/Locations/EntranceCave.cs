using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class EntranceCave : AdventureLocation
    {
        public EntranceCave(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Cave1;
            Name = "Entrance Cave";
            ShortDescription = "below the grate";
            LongDescription = "in a small chamber beneath a 3x3 steel grate, with a rusty ladder leading to the surface. A low crawl over cobbles leads inward to the west.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem> { ItemFactory.GetInstance(Game, Item.Grate) };
            ValidMoves = new List<PlayerMove> {
                new PlayerMove(string.Empty, Location.Cobbles, "west", "w"),
            };
        }
    }
}
