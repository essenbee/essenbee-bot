using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Cobbles : AdventureLocation
    {
        public Cobbles(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = "cobbles";
            Name = "Cobble Crawl";
            ShortDescription = "in cobble crawl";
            LongDescription = "crawling over cobbles in a low passage. There is some very feint light visible at the east end of the passage.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, string>
            {
                { "e", "cave1" },
                { "u", "cave1" },
                { "west", "debris" },
                { "w", "debris" },
            };
        }
    }
}
