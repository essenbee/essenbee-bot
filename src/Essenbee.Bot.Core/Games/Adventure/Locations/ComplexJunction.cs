using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class ComplexJunction : AdventureLocation
    {
        public ComplexJunction(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.ComplexJunction;
            Name = "complex junction";
            ShortDescription = "at complex junction";
            LongDescription = "a complex junction. A low hands and knees passage from the " +
                "North joins a higher crawl from the east to make a walking passage " + 
                "going West. There is also a large room above. The air is damp here.";
            Items = new List<IAdventureItem>();
            Level = 1;
            IsDark = true;
            IsSpawnPoint = true;
            SpawnType = MonsterGroup.Dwarves;
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.DustyCave, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.Bedquilt, "west", "w", "bedquilt"),
                //new PlayerMove(string.Empty, Location., "north", "n"), // shell room
                new PlayerMove(string.Empty, Location.Anteroom, "east", "e"),
            };
        }
    }
}
