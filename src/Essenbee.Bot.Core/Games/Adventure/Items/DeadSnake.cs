using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class DeadSnake : AdventureItem
    {
        internal DeadSnake(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.DeadSnake;
            Name = "large dead snake";
            PluralName = "large dead snakes";
        }
    }
}
