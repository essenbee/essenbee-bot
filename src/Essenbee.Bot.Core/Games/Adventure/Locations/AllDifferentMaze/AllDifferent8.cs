using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent8 : AdventureLocation
    {
        public AllDifferent8(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent8;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a twisty maze of little passages, all different";
            LongDescription = "in a twisty maze of little passages, all different";
            IsDark = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent6, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent7, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent5, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent11, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent10, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent4, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent9, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent1, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent3, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent2, "north", "n"),
            };
        }
    }
}

