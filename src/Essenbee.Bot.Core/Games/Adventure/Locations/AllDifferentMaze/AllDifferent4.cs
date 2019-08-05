using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent4 : AdventureLocation
    {
        public AllDifferent4(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent4;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a little maze of twisty passages, all different";
            LongDescription = "in a little maze of twisty passages, all different";
            IsDark = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent11, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent6, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent8, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent9, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent2, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent3, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent1, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent10, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent7, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent5, "north", "n"),
            };
        }
    }
}

