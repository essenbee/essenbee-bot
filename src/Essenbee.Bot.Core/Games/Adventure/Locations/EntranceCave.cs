using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class EntranceCave : AdventureLocation
    {
        public EntranceCave(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = "cave1";
            Name = "Entrance Cave";
            ShortDescription = "below the grate";
            LongDescription = "in a small chamber beneath a 3x3 steel grate to the surface. A low crawl over cobbles leads inward to the west.";
            WaterPresent = false;
            IsDark = true;
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, string>();
        }
    }
}
