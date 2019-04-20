using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Vase : AdventureItem
    {
        internal Vase(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Vase;
            Name = "delicate precious Ming *vase*";
            PluralName = "delicate precious Ming *vases*";
            IsPortable = true;
            IsTreasure = true;

            var smash = new ItemInteraction(Game, "smash", "break");
            smash.RegisteredInteractions.Add(new Display("You smash the vase and pieces fly everywhere!"));
            smash.RegisteredInteractions.Add(new RemoveFromInventory());
            smash.RegisteredInteractions.Add(new AddToLocation(new BrokenVase(Game)));
            Interactions.Add(smash);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
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
