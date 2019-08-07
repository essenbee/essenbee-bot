using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent11 : AdventureLocation
    {
        public AllDifferent11(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent11;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a maze of little twisty passages, all different";
            LongDescription = "in a maze of little twisty passages, all different";
            IsDark = true;
            NoBack = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllDifferent7, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent10, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent1, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent2, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent8, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent9, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent3, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent4, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent5, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent6, "north", "n"),
            };
        }
    }
}

