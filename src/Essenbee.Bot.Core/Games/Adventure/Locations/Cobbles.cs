using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Cobbles : AdventureLocation
    {
        public Cobbles(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Cobbles;
            Name = "Cobble Crawl";
            ShortDescription = "in cobble crawl";
            LongDescription = "crawling over cobbles in a low passage. There is some very feint light visible at the east end of the passage.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem> { ItemFactory.GetInstance(Game, Item.Cage) };
            ValidMoves = new List<PlayerMove> {
                new PlayerMove(Location.Cave1, "east", "e"),
                 new PlayerMove(Location.Debris, "west", "w"),
            };
        }
    }
}
