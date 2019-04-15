using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Rug : AdventureItem
    {
        internal Rug(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Rug;
            Name = "Persian *rug*";
            PluralName = "Persian *rugs*";
            IsPortable = true;
        }
    }
}
