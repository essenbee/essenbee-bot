namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class ShardOfGlass : AdventureItem
    {
        internal ShardOfGlass(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.ShardOfGlass;
            Name = "*shard* of jagged glass";
            PluralName = "*shards* of jagged glass";
            IsPortable = true;
            IsEndlessSupply = true;
        }
    }
}
