using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class FissureWest : AdventureLocation
    {
        public FissureWest(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.FissureWest;
            Name = "West Bank of Fissure";
            ShortDescription = "on the edge of a deep fissure";
            LongDescription = "standing on the western side of a wide fissure in the rock.";
            IsDark = true;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(game, Item.Diamond) };
            ValidMoves = new List<IPlayerMove> 
            {
                new PlayerMove("", Location.HallOfMistsWest, "north", "n", "w", "west"),
            };
        }
    }
}
