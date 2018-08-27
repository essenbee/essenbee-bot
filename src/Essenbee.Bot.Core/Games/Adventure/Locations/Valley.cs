using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Valley : AdventureLocation
    {
        public Valley(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Valley;
            Name = "Valley";
            ShortDescription = "in a valley";
            LongDescription = "in a valley in the forest beside a stream tumbling along a rocky bed.";
            WaterPresent = true;
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, Location> {
                        {"north", Location.Road },
                        {"n", Location.Road },
                        {"upstream", Location.Road },
                        {"south", Location.Slit },
                        {"downstream", Location.Slit },
                        {"s", Location.Slit },
                        {"forest", Location.Forest },
                        {"east", Location.Forest },
                        {"e", Location.Forest },
                        {"west", Location.Forest },
                        {"w", Location.Forest },
                        {"up", Location.Forest },
                };
        }
    }
}
