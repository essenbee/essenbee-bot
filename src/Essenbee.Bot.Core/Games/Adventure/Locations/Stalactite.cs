using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class Stalactite : AdventureLocation
    {
        public Stalactite(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.StalactiteRoom;
            Name = "Stalactite Room";
            ShortDescription = "at top of stalactite";
            LongDescription = "in a cave where large stalactite extends from the roof and almost reaches the floor of a cave " + 
                "below. You could climb down it, and jump from it, but having done so you would be unable " + 
                "to reach it to climb back up...";
            IsDark = true;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.Cage) };
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove("You climb down the stalactite and jump down...", Location.AllAlike7, "down", "d"),

            };
        }
    }
}
