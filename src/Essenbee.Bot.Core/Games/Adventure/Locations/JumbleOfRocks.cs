using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class JumbleOfRocks : AdventureLocation
    {
        public JumbleOfRocks(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.JumbleOfRocks;
            Name = "Jumble of Rocks";
            ShortDescription = "in a jumble of rocks, with cracks everywhere";
            LongDescription = "in a jumble of rocks, with cracks everywhere.";
            IsDark = true;
            Level = 1;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Cage) };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You drop down into a narrow crack...", Location.Y2, "down", "d"),
                new PlayerMove("You squeeze up through a jagged crack...", Location.HallOfMistsEast, "up", "u", "climb"),
            };
        }
    }
}
