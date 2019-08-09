using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Key : AdventureItem
    {
        internal Key(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Key;
            Name = "large iron *key*";
            PluralName = "large iron *keys*";
            IsPortable = true;
            Slots = 0;
        }
    }
}
