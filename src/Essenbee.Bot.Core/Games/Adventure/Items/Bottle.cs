using Essenbee.Bot.Core.Games.Adventure.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Bottle : AdventureItem
    {
        public IReadonlyAdventureGame Game { get; }

        public Bottle(IReadonlyAdventureGame game)
        {
            Game = game;
            ItemId = "bottle";
            Name = "small glass *bottle*";
            PluralName = "small glass *bottles*";
            IsPortable = true;

            var smash = new ItemInteraction(Game, "smash", "break");
            smash.RegisteredInteractions.Add(new Display("You smash the bottle and glass flies everywhere!"));
            smash.RegisteredInteractions.Add(new RemoveFromInventory());
            smash.RegisteredInteractions.Add(new AddToLocation(new BrokenGlass(Game)));

            Interactions.Add(smash);
        }
    }
}
