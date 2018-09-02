﻿using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Locations
{
    public class WestSideChamber : AdventureLocation
    {
        public WestSideChamber(IReadonlyAdventureGame game) : base(game)
        {
            LocationId = Location.WestSideChamber;
            Name = "West Side Chamber";
            ShortDescription = "in west side chamber";
            LongDescription = "in the west side chamber of the Hall of the Mountain King. A passage continues west and up here.";
            Items = new List<IAdventureItem>(); // Coins
            ValidMoves = new List<IPlayerMove>
            {
                new PlayerMove(string.Empty, Location.HallOfMountainKing, "east", "e"),
                // new PlayerMove(string.Empty, Location., "west", "w", "up"),
            };
        }
    }
}
