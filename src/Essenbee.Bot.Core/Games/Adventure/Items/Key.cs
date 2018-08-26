namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Key : AdventureItem
    {
        internal Key(IReadonlyAdventureGame game) : base(game)
        {
            ItemId = "key";
            Name = "large iron *key*";
            PluralName = "large iron *keys*";
            IsPortable = true;
        }
    }
}
