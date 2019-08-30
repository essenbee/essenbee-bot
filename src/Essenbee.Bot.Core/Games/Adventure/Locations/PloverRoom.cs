using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public partial class PloverRoom : AdventureLocation
    {
        public PloverRoom(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.PloverRoom;
            Name = "Plover Room";
            ShortDescription = "in Plover Room";
            LongDescription = "You're in a small chamber lit by an eerie green light. An extremely " +
                              "narrow tunnel exits to the west. A dark corridor leads NE.";
            Level = 1;
            IsDark = false;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Emerald) }; ;
            ValidMoves = new List<IPlayerMove> {
                new SqueezeMove("You breathe in and make yourself at thin as possible...", Location.Alcove, "west", "w"),
                new PlayerMove(string.Empty, Location.DarkRoom, "northeast", "ne"),
            };
        }
    }
}
