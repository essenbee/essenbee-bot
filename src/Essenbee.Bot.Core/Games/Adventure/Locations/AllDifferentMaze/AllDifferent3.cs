using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent3 : AdventureLocation
    {
        public AllDifferent3(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent3;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a maze of twisting little passages, all different";
            LongDescription = "in a maze of twisting little passages, all different";
            IsDark = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent9, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent11, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent6, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent7, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent4, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent8, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent5, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent2, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent1, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent10, "north", "n"),
            };
        }
    }
}

