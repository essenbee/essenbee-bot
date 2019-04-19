using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class AllAlike11 : AdventureLocation
    {
        public AllAlike11(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.AllAlike11;
            Name = "Maze of Tunnels, All Alike";
            ShortDescription = "in a maze";
            LongDescription = "in a maze of twisty little passages, all alike.";
            IsDark = true;
            Items = new List<IAdventureItem>();
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("", Location.AllAlike12, "north", "n"),
                new PlayerMove("", Location.AllAlike11, "south", "s"),
                new PlayerMove("", Location.AllAlike9, "east", "e"),
                new PlayerMove("", Location.AllAlike7, "west", "w"),
                new PlayerMove("", Location.AllAlike10, "up", "u", "climb"),
                // Dead end? new PlayerMove("", Location.AllAlike2, "down", "d"),
            };
        }
    }
}
