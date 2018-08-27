using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Grate : AdventureItem
    {
        internal Grate(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Grate;
            Name = "strong steel grate";
            PluralName = "strong steel grates";
            IsPortable = false;
            IsOpen = false;
            IsLocked = true;
            ItemIdToUnlock = Item.Key;

            var open = new ItemInteraction(Game, "open");
            open.RegisteredInteractions.Add(new Open());
            Interactions.Add(open);

            var found = Game.TryGetLocation(Location.Depression, out var depression);

            var unlock = new ItemInteraction(Game, "unlock");
            unlock.RegisteredInteractions.Add(new Unlock());
            unlock.RegisteredInteractions.Add(new Display("You open the grate and see a dark space below it. A rusty iron ladder leads down into pitch blackness!"));

            if (found)
            {
                unlock.RegisteredInteractions.Add(new AddMoves(new Dictionary<string, Location>
                {
                    { "down", Location.Cave1 },
                    { "d", Location.Cave1 },
                }, depression));
            }

            found = Game.TryGetLocation(Location.Cave1, out var entranceCave);

            if (found)
            {
                unlock.RegisteredInteractions.Add(new AddMoves(new Dictionary<string, Location>
                {
                    { "up", Location.Depression},
                    { "u", Location.Depression },
                }, entranceCave));
            }

            Interactions.Add(unlock);
        }
    }
}
