using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public partial class PloverRoom
    {
        public class DarkRoom : AdventureLocation
        {
            public DarkRoom(IReadonlyAdventureGame game) : base(game)
            {
                LocationId = Location.DarkRoom;
                Name = "Dark Room";
                ShortDescription = "in the Dark Room";
                LongDescription = "in the Dark room. A corridor leading south is the only exit.";
                Level = 1;
                IsDark = true;
                Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.PlatinumPyramid) };
                ValidMoves = new List<IPlayerMove> {
                    new PlayerMove(string.Empty, Location.PloverRoom, "south", "s"),
                };
            }
        }
    }
}
