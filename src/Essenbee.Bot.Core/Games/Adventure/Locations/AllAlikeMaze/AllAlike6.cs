using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike6 : AdventureLocation
    {
        public AllAlike6(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike6;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllAlike5, "up", "u", "climb"),
                new PlayerMove("", Location.AllAlike5, "down", "d"),
            };
        }
    }
}
