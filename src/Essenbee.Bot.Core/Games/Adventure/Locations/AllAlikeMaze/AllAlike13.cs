using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike13 : AdventureLocation
    {
        public AllAlike13(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike13;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.BrinkOfPit, "south", "s"),
                new PlayerMove("", Location.AllAlike14, "east", "e"),
                // Dead end? new PlayerMove("", Location.AllAlike2, "west", "w"),
            };
        }
    }
}
