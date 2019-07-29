using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class SecretNorthEastCanyon : AdventureLocation
    {
        public SecretNorthEastCanyon(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.SecretNorthEastCanyon;
            Name = "secret canyon";
            ShortDescription = "in a secret canyon which exits to the north and east";
            LongDescription = "in a secret canyon which exits to the north and east.";
            Level = 1;
            IsDark = true;

            Items = new List<IAdventureItem> 
            {
                ItemFactory.GetInstance(Game, Item.Dragon),
                ItemFactory.GetInstance(Game, Item.Rug),
            };

            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove("A wicked green dragon is blocking your way! It hisses at you menacingly.", Location.SecretNorthEastCanyon, "north", "n"),
                new PlayerMove(string.Empty, Location.SecretEastWestCanyon, "east", "e"),
            };
        }
    }
}
