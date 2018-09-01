using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Items;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Debris : AdventureLocation
    {
        public Debris(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Debris;
            Name = "Debris room";
            ShortDescription = "in debris room";
            LongDescription = "in a debris room filled with stuff washed in from the surface. A low wide passage with cobbles becomes plugged with mud and debris" +
                " here, but an awkward canyon leads upward and to the west. Scrawled on the wall is a cryptic note: `XYZZY`.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem> { ItemFactory.GetInstance(Game, Item.Rod) };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.Cobbles, "east", "e"),
                new PlayerMove(string.Empty, Location.Canyon, "west", "w", "up"),
            };
        }
    }
}
