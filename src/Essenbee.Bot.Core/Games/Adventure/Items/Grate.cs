namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Grate : AdventureItem
    {
        public Grate(IReadonlyAdventureGame game) : base(game)
        {
            ItemId = "grate";
            Name = "strong steel grate";
            PluralName = "strong steel grates";
            IsPortable = false;
            IsOpen = false;
            IsLocked = true;
            ItemIdToUnlock = "key";
        }
    }
}
