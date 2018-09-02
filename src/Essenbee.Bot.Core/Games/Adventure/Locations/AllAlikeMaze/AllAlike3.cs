using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike3 : AdventureLocation
    {
        public AllAlike3(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike3;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllAlike4, "south", "s"),
                new PlayerMove("", Location.AllAlike5, "east", "e"),
                new PlayerMove("", Location.AllAlike1, "west", "w"),
            };
        }
    }
}
