using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike7 : AdventureLocation
    {
        public AllAlike7(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike7;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Level = 1;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllAlike11, "south", "s"),
                new PlayerMove("", Location.AllAlike4, "east", "e"),
                new PlayerMove("", Location.AllAlike8, "west", "w"),
                new PlayerMove("", Location.AllAlike9, "down", "d"),
            };
        }
    }
}
