using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllDifferent1 : AdventureLocation
    {
        public AllDifferent1(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllDifferent1;
            Name = "Maze of Tunnels, All Different";
            ShortDescription = "in a maze of twisty little passages, all different";
            LongDescription = "in a maze of twisty little passages, all different";
            IsDark = true;
            NoBack = true;
            Items = new List<IAdventureItem>();
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You follow a long winding tunnel...", Location.LongHallWest, "down", "d"),
                new PlayerMove(string.Empty, Location.AllDifferent3, "south", "s"),
                new PlayerMove(string.Empty, Location.AllDifferent4, "southwest", "sw"),
                new PlayerMove(string.Empty, Location.AllDifferent5, "northeast", "ne"),
                new PlayerMove(string.Empty, Location.AllDifferent6, "southeast", "se"),
                new PlayerMove(string.Empty, Location.AllDifferent7, "up", "u", "climb"),
                new PlayerMove(string.Empty, Location.AllDifferent8, "northwest", "nw"),
                new PlayerMove(string.Empty, Location.AllDifferent9, "east", "e"),
                new PlayerMove(string.Empty, Location.AllDifferent10, "west", "w"),
                new PlayerMove(string.Empty, Location.AllDifferent11, "north", "n"),
            };
        }
    }
}

