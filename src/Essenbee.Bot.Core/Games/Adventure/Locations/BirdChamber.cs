using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class BirdChamber : AdventureLocation
    {
        public BirdChamber(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.BirdChamber;
            Name = "Bird Chamber";
            ShortDescription = "in bird chamber";
            LongDescription = "You are in a splendid chamber thirty feet high. The walls are frozen rivers of orange flowstone." + 
                              " An awkward canyon and a good passage exit from east and west sides of the chamber.";
            IsDark = true;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Bird) };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.Canyon, "east", "e"),
                new PlayerMove(string.Empty, Location.SmallPit, "west", "w"),
            };
        }
    }
}
