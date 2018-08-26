using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Mailbox : AdventureItem
    {
        public Mailbox(IReadonlyAdventureGame game) : base(game)
        {
            ItemId = "mailbox";
            Name = "small mailbox";
            PluralName = "small mailboxes";
            IsOpen = true;
            IsContainer = true;
            Contents = new List<AdventureItem> { new Leaflet(Game) };
        }
    }
}
