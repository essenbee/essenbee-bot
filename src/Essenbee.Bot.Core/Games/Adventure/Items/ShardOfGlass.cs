namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class ShardOfGlass : AdventureItem
    {
        public IReadonlyAdventureGame Game { get; }

        public ShardOfGlass(IReadonlyAdventureGame game)
        {
            Game = game;
            ItemId = "shard";
            Name = "*shard* of jagged glass";
            PluralName = "*shards* of jagged glass";
            IsPortable = true;
            IsEndlessSupply = false; // ToDo: how to handle endless items
        }
    }
}
