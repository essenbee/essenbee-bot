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
            Moves = new Dictionary<string, Location> {
                        {"north", Location.Slit },
                        {"n", Location.Slit },
            };
        }
    }
}
