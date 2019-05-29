using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class DeadDragon : AdventureItem
    {
        private static DeadDragon _instance = null;
        private static object _mutex = new object();
        public static DeadDragon GetInstance(IReadonlyAdventureGame game, params string[] nouns)
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new DeadDragon(game, nouns);
                    }
                }
            }

            return _instance;
        }
        private DeadDragon(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.DeadDragon;
            Name = "body of a huge green dead dragon lying off to one side";
            PluralName = "body of a huge green dead dragon lying off to one side";
            IsPortable = false;
        }
    }
}
