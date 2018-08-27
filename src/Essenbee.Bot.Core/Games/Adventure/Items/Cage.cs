using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Cage : AdventureItem
    {
        internal Cage(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Cage;
            Name = "wicker *cage*";
            PluralName = "wicker *cages*";
            IsOpen = true;
            IsContainer = true;
            Contents = new List<AdventureItem>();
            IsPortable = true;

            var open = new ItemInteraction(Game, "open");
            open.RegisteredInteractions.Add(new Open());
            Interactions.Add(open);

            var close = new ItemInteraction(Game, "close", "shut");
            close.RegisteredInteractions.Add(new Close());
            Interactions.Add(close);
        }
    }
}
