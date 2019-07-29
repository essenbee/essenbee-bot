using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SoftRoom : AdventureLocation
    {
        public SoftRoom(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SoftRoom;
            Name = "soft room";
            ShortDescription = "in the soft room.";
            LongDescription = "in the soft room. The walls are covered with heavy curtains, " +
                "the floor with a thick pile carpet. Moss covers the ceiling.";
            Level = 1;
            IsDark = true;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Pillow) };

            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.SwissCheese, "west", "w"),
            };
        }
    }
}
