using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent6 : AdventureLocation
    {
        public AllDifferent6(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent6;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a twisting little maze of passages, all different";
            LongDescription = "in a twisting little maze of passages, all different";
            IsDark = true;
            NoBack = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent8, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent9, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent2, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent1, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent5, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent10, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent4, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent7, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent11, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent3, "north", "n"),
            };
        }
    }
}

