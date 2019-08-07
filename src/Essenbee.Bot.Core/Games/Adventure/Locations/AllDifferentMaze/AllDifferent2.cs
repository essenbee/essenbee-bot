using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent2 : AdventureLocation
    {
        public AllDifferent2(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent2;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a little maze of twisting passages, all different";
            LongDescription = "in a little maze of twisting passages, all different";
            IsDark = true;
            NoBack = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent10, "down", "d"),
                new PlayerMove(string.Empty, Location.VendingMachine, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent3, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent8, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent7, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent11, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent6, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent5, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent9, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent4, "north", "n"),
            };
        }
    }
}

