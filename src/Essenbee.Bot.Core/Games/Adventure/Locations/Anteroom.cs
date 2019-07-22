using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Anteroom : AdventureLocation
    {
        public Anteroom(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Anteroom;
            Name = "anteroom";
            ShortDescription = "in anteroom.";
            LongDescription = "in an anteroom leading to a large passage to the East. Small " +
                "passages go West and up. The remnants of recent digging are evident.";
            Level = 1;

            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Magazines) };

            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.Bedquilt, "west", "w"),
                //new PlayerMove(string.Empty, Location., "east", "e"),  // Witts End
                new PlayerMove(string.Empty, Location.ComplexJunction, "up", "u", "climb"),
            };
        }
    }
}
