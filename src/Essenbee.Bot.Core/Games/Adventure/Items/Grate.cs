using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Grate : AdventureItem
    {
        private static Grate _instance = null;
        private static object _mutex = new object();

        public static Grate GetInstance(IReadonlyAdventureGame game, params string[] nouns)
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new Grate(game, nouns);
                    }
                }
            }

            return _instance;
        }

        private Grate(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
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

            var unlock = new ItemInteraction(Game, "unlock");
            unlock.RegisteredInteractions.Add(new Unlock());
            unlock.RegisteredInteractions.Add(new DisplayForLocation("You open the grate and see a dark space below it. A rusty iron ladder leads down into pitch blackness!", Location.Depression));
            unlock.RegisteredInteractions.Add(new DisplayForLocation("You open the grate and see the way out of the caves above you. A rusty iron ladder leads up to the daylight!", Location.Cave1));
            unlock.RegisteredInteractions.Add(new AddMoves(new List<PlayerMove>
            {
               { new PlayerMove(Location.Cave1, "down", "d", "ladder", "underground") },
            }, Game, Location.Depression));
            unlock.RegisteredInteractions.Add(new AddMoves(new List<PlayerMove>
{
               { new PlayerMove(Location.Depression, "up", "ladder", "surface") },
            }, Game, Location.Cave1));
            
            Interactions.Add(unlock);
        }
    }
}
