using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class VendingMachine : AdventureLocation
    {
        public VendingMachine(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.VendingMachine;
            Name = "Vending Machine Cave";
            ShortDescription = "in a small cave with a vending machine in it";
            LongDescription = "in a small cave with a vending machine in it";
            IsDark = true;
            Items = new List<IAdventureItem> { ItemFactory.GetInstance(Game, Item.VendingMachine)};
            Level = 1;
            ValidMoves = new List<IPlayerMove> {
                new PlayerMove(string.Empty, Location.AllDifferent2, "north", "n"),
            };
        }
    }
}

