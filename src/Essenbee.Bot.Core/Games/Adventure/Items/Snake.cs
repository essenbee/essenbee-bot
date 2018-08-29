using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Snake : AdventureItem
    {
        internal Snake(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Snake;
            Name = "large vicious-looking snake barring your way";
            PluralName = "large vicious-looking snakes barring your way";
        }
    }
}
