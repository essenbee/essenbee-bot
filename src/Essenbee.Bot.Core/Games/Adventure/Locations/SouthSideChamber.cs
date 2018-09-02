using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SouthSideChamber : AdventureLocation
    {
        public SouthSideChamber(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SouthSideChamber;
            Name = "South Side Chamber";
            ShortDescription = "in south side chamber";
            LongDescription = "in the south side chamber of the Hall of the Mountain King.";
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Jewellry) };
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.HallOfMountainKing, "north", "n"),

            };
        }
    }
}
