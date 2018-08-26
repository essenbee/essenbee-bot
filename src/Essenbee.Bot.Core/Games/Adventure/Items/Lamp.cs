using Essenbee.Bot.Core.Games.Adventure.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Lamp : AdventureItem
    {
        public IReadonlyAdventureGame Game { get; }

        public Lamp(IReadonlyAdventureGame game)
        {
            ItemId = "lamp";
            Name = "battered *lamp*";
            PluralName = "battered *lamps*";
            IsPortable = true;
            IsEndlessSupply = false; // ToDo: how to handle endless items

            var light = new ItemInteraction(Game, "light");
            light.RegisteredInteractions.Add(new ActivateItem("The lamp shines brightly."));
            light.RegisteredInteractions.Add(new AddPlayerStatus(PlayerStatus.HasLight));

            Interactions.Add(light);
            Game = game;
        }
    }
}
