using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Forest : AdventureLocation
    {
        public Forest(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Forest;
            Name = "Forest";
            ShortDescription = "in open forest";
            LongDescription = "in open forest, with a deep valley to one side.";
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, Location>
            {
                {"valley", Location.Valley },
                {"east", Location.Valley },
                {"e", Location.Valley },
                {"down", Location.Valley },
                {"d", Location.Valley },
                {"west", Location.Forest },
                {"south", Location.Forest },
                {"w", Location.Forest },
                {"s", Location.Forest },

                // ToDo: if forest, north or forward, 50% -> forest, 50% - forest2
             };
        }
    }
}
