using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Hill : AdventureLocation
    {
        public Hill(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.Hill;
            Name = "Hill";
            ShortDescription = "atop a hill";
            LongDescription = "atop hill, still in the forest. The road slopes back down the other side of the hill.There is a building in the distance.";
            Items = new List<AdventureItem>();
            Moves = new Dictionary<string, Location>
            {
                {"road", Location.Road },
                {"east", Location.Road },
                {"e", Location.Road },
                {"forward", Location.Road },
                {"north", Location.Forest },
                {"south", Location.Forest },
                {"n", Location.Forest },
                {"s", Location.Forest },
             };
        }
    }
}
