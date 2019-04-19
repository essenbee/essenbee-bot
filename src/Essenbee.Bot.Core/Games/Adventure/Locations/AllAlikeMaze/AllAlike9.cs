using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike9 : AdventureLocation
    {
        public AllAlike9(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike9;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllAlike10, "south", "s"),
                new PlayerMove("", Location.AllAlike11, "east", "e"),
                new PlayerMove("", Location.AllAlike8, "west", "w"),
                new PlayerMove("", Location.AllAlike7, "up", "u", "climb"),
            };
        }
    }
}
