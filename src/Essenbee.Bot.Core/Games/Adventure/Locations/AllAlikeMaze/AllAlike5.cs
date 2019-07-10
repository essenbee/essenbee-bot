using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike5 : AdventureLocation
    {
        public AllAlike5(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike5;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Level = 1;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                // Dead end? new PlayerMove("", Location.AllAlike5, "south", "s"),
                // Dead end? new PlayerMove("", Location.AllAlike3, "east", "e"),
                new PlayerMove("", Location.AllAlike1, "west", "w"),
                new PlayerMove("", Location.AllAlike6, "up", "u", "climb"),
                new PlayerMove("", Location.AllAlike6, "down", "d"),
            };
        }
    }
}
