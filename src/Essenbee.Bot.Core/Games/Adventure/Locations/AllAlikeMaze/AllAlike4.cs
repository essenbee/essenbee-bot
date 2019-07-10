using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike4 : AdventureLocation
    {
        public AllAlike4(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike4;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Level = 1;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                // Dead end? new PlayerMove("", Location.AllAlike1, "north", "n"),
                new PlayerMove("", Location.AllAlike7, "south", "s"),
                new PlayerMove("", Location.AllAlike3, "east", "e"),
                // Dead end? new PlayerMove("", Location.AllAlike1, "down", "d"),
            };
        }
    }
}
