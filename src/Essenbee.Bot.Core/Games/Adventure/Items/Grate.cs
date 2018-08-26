using Essenbee.Bot.Core.Games.Adventure.Interactions;
using System.Collections.Generic;

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
            unlock.RegisteredInteractions.Add(new Display("You open the grate and see a dark space below it. A rusty iron ladder leads down into pitch blackness!"));
            unlock.RegisteredInteractions.Add(new AddMoves(new Dictionary<string, string> 
            {
                { "down", "cave1" },
                { "d", "cave1" },
            }));
            Interactions.Add(unlock);
        }
    }
}
