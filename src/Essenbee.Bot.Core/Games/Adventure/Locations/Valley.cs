using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Valley : AdventureLocation
    {
        public Valley(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = "valley";
            Name = "Valley";
            ShortDescription = "in a valley";
            LongDescription = "in a valley in the forest beside a stream tumbling along a rocky bed.";
            WaterPresent = true;
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, string> {
                        {"north", "road" },
                        {"south", "slit" },
                        {"n", "road" },
                        {"s", "slit" },
                };
        }
    }
}
