using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class DeadDragon : AdventureItem
    {

        internal DeadDragon(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.DeadDragon;
            Name = "The body of a huge green dead dragon is lying off to one side.";
            PluralName = "The body of a huge green dead dragon is lying off to one side.";
            IsPortable = false;
        }
    }
}
