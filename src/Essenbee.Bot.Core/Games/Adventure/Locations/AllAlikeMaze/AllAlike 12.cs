using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike12 : AdventureLocation
    {
        public AllAlike12(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike12;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllAlike12, "north", "n"),
                new PlayerMove("", Location.BrinkOfPit, "east", "e"),
                new PlayerMove("", Location.AllAlike11, "west", "w"),
                // Dead end? new PlayerMove("", Location.AllAlike5, "down", "d")
            };
        }
    }
}
