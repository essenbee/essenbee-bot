using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Start : AdventureLocation
    {
        public Start(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = "road";
            Name = "End of a Road";
            ShortDescription = "standing at the end of a road.";
            LongDescription = "standing at the end of a road before a small brick building. Around you is a forest.  A small stream flows out of the building and down a gully.";
            Items = new List<AdventureItem> { ItemFactory.GetInstance(Game, "mailbox") };
            Moves = new Dictionary<string, string>
                    {
                        {"east", "building" },
                        {"e", "building" },
                        {"enter", "building" },
                        {"in", "building" },
                        {"inside", "building" },
                        {"building", "building" },
                        {"south" , "valley" },
                        {"s" , "valley" },
                    };
        }
    }
}
