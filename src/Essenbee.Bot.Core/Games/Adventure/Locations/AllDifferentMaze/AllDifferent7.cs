using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent7 : AdventureLocation
    {
        public AllDifferent7(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent7;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a twisty little maze of passages, all different";
            LongDescription = "in a twisty little maze of passages, all different";
            IsDark = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent4, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent5, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent9, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent10, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent3, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent2, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent11, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent6, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent8, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent1, "north", "n"),
            };
        }
    }
}

