using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent10 : AdventureLocation
    {
        public AllDifferent10(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent10;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a maze of little twisting passages, all different";
            LongDescription = "in a maze of little twisting passages, all different";
            IsDark = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent1, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent8, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent11, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent4, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent9, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent5, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent2, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent3, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent6, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent7, "north", "n"),
            };
        }
    }
}

