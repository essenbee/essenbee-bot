using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class CrystalBridge : AdventureItem
    {
        private static CrystalBridge _instance = null;
        private static object _mutex = new object();
        public static CrystalBridge GetInstance(IReadonlyAdventureGame game, params string[] nouns)
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new CrystalBridge(game, nouns);
                    }
                }
            }

            return _instance;
        }

        private CrystalBridge(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Cage;
            Name = "crystal bridge";
            PluralName = "crystal bridges";
            Contents = new List<AdventureItem>();
            IsActive = true;
        }
    }
}
