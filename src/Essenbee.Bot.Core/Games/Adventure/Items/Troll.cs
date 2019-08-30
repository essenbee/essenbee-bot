using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Troll : AdventureItem
    {
        internal Troll(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Snake;
            Name = "large troll barring your way";
            PluralName = "large trolls barring your way";

            var kill = new ItemInteraction(Game, "kill", "slay", "murder", "attack");
            Interactions.Add(kill);
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
