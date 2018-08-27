using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Forest2 : AdventureLocation
    {
        public Forest2(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Forest2;
            Name = "Forest";
            ShortDescription = "in open forest";
            LongDescription = "in open forest near both a valley and a road.";
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, Location>
            {
                {"road", Location.Road },
                {"north", Location.Road },
                {"n", Location.Road },
                {"down", Location.Valley },
                {"d", Location.Valley },
                {"valley", Location.Valley },
                {"west", Location.Valley },
                {"w", Location.Valley },
                {"east", Location.Valley },
                {"e", Location.Valley },
                {"south", Location.Forest },
                {"s", Location.Forest },
                {"forest", Location.Forest },
             };
        }
    }
}