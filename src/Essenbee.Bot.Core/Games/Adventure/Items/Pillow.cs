using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Pillow : AdventureItem
    {
        internal Pillow(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Pillow;
            Name = "velvet *pillow*";
            PluralName = "velvet *pillows*";
            IsPortable = true;
        }
    }
}
