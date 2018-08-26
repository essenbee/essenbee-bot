using Essenbee.Bot.Core.Games.Adventure.Interactions;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Grate : AdventureItem
    {
        internal Grate(IReadonlyAdventureGame game) : base(game)
        {
            ItemId = "grate";
            Name = "strong steel grate";
            PluralName = "strong steel grates";
            IsPortable = false;
            IsOpen = false;
            IsLocked = true;
            ItemIdToUnlock = "key";

            var open = new ItemInteraction(Game, "open");
            open.RegisteredInteractions.Add(new Open());
            Interactions.Add(open);

            var unlock = new ItemInteraction(Game, "unlock");
            unlock.RegisteredInteractions.Add(new Unlock());
            unlock.RegisteredInteractions.Add(new Display("You open the grate and see a dark spce below it. A rusty iron ladder leads down into pitch blackness!"));
            Interactions.Add(unlock);
        }
    }
}
