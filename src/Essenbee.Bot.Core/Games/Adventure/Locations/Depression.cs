using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Depression : AdventureLocation
    {
        public Depression(IReadonlyAdventureGame game) : base(game)
        {
            var grate = ItemFactory.GetInstance(Game, Item.Grate);

            LocationId = Location.Depression;
            Name = "Depression";
            ShortDescription = "outside the grate";
            LongDescription = "in a 20-foot depression floored with bare dirt. Set into the dirt is a strong steel grate mounted in concrete. A dry streambed leads into the depression.";
            Items = new List<AdventureItem> { grate, };
            ValidMoves = new List<PlayerMove> {
                new PlayerMove(string.Empty, Location.Forest2, "east", "e", "south", "s"),
                new PlayerMove(string.Empty, Location.Forest, "west", "w"),
                new PlayerMove(string.Empty, Location.Slit, "north", "n"),
            };
        }
    }
}
