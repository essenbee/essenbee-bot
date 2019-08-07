using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent5 : AdventureLocation
    {
        public AllDifferent5(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent5;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a twisting maze of little passages, all different";
            LongDescription = "in a twisting maze of little passages, all different";
            IsDark = true;
            NoBack = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent3, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent2, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent7, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent10, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent11, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent1, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent1, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent8, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent4, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent9, "north", "n"),
            };
        }
    }
}

