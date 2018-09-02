using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike1 : AdventureLocation
    {
        public AllAlike1(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike1;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You follow a long winding tunnel...", Location.HallOfMistsWest, "up"),
                new PlayerMove("", Location.AllAlike1, "north", "n"),
                new PlayerMove("", Location.AllAlike5, "south", "s"),
                new PlayerMove("", Location.AllAlike3, "east", "e"),
                new PlayerMove("", Location.AllAlike2, "west", "w"),
            };
        }
    }
}
