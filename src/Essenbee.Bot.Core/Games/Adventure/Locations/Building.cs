using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Building : AdventureLocation
    {
        public Building(IReadonlyAdventureGame game) : base(game)
        {
            var bottle = ItemFactory.GetInstance(Game, Item.Bottle);
            var lamp = ItemFactory.GetInstance(Game, Item.Lamp);
            var key = ItemFactory.GetInstance(Game, Item.Key);
            var food = ItemFactory.GetInstance(Game, Item.FoodRation);
            var water = ItemFactory.GetInstance(Game, Item.Water);

            LocationId = Location.Building;
            Name = "Small Brick Building";
            ShortDescription = "inside a small brick building.";
            LongDescription = " inside a small brick building, a well house for a bubbling spring.";
            WaterPresent = true;

            Items = new List<IAdventureItem>
            {
                key,
                lamp,
                bottle,
                food,
                water,
            };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You leave the building.", Location.Road, "west", "w", "road", "out", "outside"),
            };
        }
    }
}
