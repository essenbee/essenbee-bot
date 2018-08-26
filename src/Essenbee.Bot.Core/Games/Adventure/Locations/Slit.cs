using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Slit : AdventureLocation
    {
        public Slit(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = "slit";
            Name = "Slit";
            ShortDescription = "at the slit in the streambed";
            LongDescription = "besides the stream. At your feet all the water of the stream splashes into a 2-inch slit in the rock. Downstream the streambed is bare rock.";
            WaterPresent = true;
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, string> {
                        {"north", "valley" },
                        {"south", "depression" },
                        {"n", "valley" },
                        {"s", "depression" },
                };
        }
    }
}
