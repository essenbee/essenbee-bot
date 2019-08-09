using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class PirateChest : AdventureItem
    {
        internal PirateChest(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Mailbox;
            Name = "chest made of wood and iron";
            PluralName = "chests made of wood and iron";
            IsOpen = false;
            IsContainer = true;
            Slots = 2;
            Contents = new List<IAdventureItem>();
        }
    }
}
