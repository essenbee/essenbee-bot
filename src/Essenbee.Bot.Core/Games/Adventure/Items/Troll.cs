using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Troll : AdventureItem
    {
        private static Troll _instance = null;
        private static object _mutex = new object();

        internal Troll(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Troll;
            Name = "large troll barring your way";
            PluralName = "large trolls barring your way";

            var kill = new ItemInteraction(Game, "kill", "slay", "murder", "attack");
            Interactions.Add(kill);
        }

        public static Troll GetInstance(IReadonlyAdventureGame game, params string[] nouns)
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    if (_instance == null)
                    {
                        _instance = new Troll(game, nouns);
                    }
                }
            }

            return _instance;
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("kill"))
                {

                }

                foreach (var action in interaction.RegisteredInteractions)
                {
                    action.Do(player, this);
                }

                return true;
            }

            return false;
        }
    }
}
