using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class VendingMachine : AdventureItem
    {
        internal VendingMachine(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.VendingMachine;
            Name = "vending machine selling batteries";
            PluralName = "vending machines selling batteries";
            IsOpen = false;
            IsContainer = false;

            var push = new ItemInteraction(Game, "push", "shove", "move", "slide");
            push.RegisteredInteractions.Add(new AddMoves(new List<IPlayerMove> 
            {
                new PlayerMove(string.Empty, Location.RoughHewn, "south", "s"),
            }, Game));
            push.RegisteredInteractions.Add(new Display("You give a mighty heave on the machine, and it slides aside, revealing a passageway heading South..."));

            Interactions.Add(push);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("push"))
                {
                    foreach (var action in interaction.RegisteredInteractions)
                    {
                        action.Do(player, this);
                    }
                }

                return true;
            }

            return false;
        }
    }
}
