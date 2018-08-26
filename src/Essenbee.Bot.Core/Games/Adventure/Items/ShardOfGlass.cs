namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class ShardOfGlass : AdventureItem
    {
        internal ShardOfGlass(IReadonlyAdventureGame game) : base(game)
        {
            ItemId = "shard";
            Name = "*shard* of jagged glass";
            PluralName = "*shards* of jagged glass";
            IsPortable = true;
            IsEndlessSupply = true;
        }
    }
}
