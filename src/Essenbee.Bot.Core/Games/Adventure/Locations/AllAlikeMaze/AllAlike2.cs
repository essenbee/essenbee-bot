using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike2 : AdventureLocation
    {
        public AllAlike2(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike2;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Level = 1;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllAlike1, "north", "n"),
                new PlayerMove("", Location.AllAlike2, "south", "s"),
                //Dead end? new PlayerMove("", Location.AllAlike3, "east", "e"), 
                new PlayerMove("", Location.AllAlike2, "west", "w"),
            };
        }
    }
}
