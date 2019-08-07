using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent9 : AdventureLocation
    {
        public AllDifferent9(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent9;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a little twisty maze of passages, all different";
            LongDescription = "in a little twisty maze of passages, all different";
            IsDark = true;
            NoBack = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent5, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent4, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent10, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent3, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent1, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent6, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent7, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent11, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent2, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent8, "north", "n"),
            };
        }
    }
}

