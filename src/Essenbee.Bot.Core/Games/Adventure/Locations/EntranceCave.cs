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
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Grate) };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You get down on your belly and start to crawl, inching your way forwards...", Location.Cobbles, "west", "w"),
            };
        }
    }
}
