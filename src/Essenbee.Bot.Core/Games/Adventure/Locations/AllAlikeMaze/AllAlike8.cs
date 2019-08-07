using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike8 : AdventureLocation
    {
        public AllAlike8(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike8;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            NoBack = true;
            Level = 1;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllAlike7, "east", "e"),
                new PlayerMove("", Location.AllAlike9, "west", "w"),
            };
        }
    }
}
