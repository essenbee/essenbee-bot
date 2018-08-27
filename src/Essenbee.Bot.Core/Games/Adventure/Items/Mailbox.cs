using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Mailbox : AdventureItem
    {
        internal Mailbox(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Mailbox;
            Name = "small mailbox";
            PluralName = "small mailboxes";
            IsOpen = true;
            IsContainer = true;
            Contents = new List<AdventureItem> { ItemFactory.GetInstance(Game, Item.Leaflet) };
        }
    }
}
