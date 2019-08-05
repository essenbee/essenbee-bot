using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Coins : AdventureItem
    {
        internal Coins(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Coins;
            Name = "pile of *coins*";
            PluralName = "piles of *coins*";
            IsPortable = true;
            IsTreasure = true;

            var pay = new ItemInteraction(Game, "use", "spend", "insert");
            pay.RegisteredInteractions.Add(new RemoveFromInventory());
            pay.RegisteredInteractions.Add(new AddToLocation(ItemFactory.GetInstance(Game, Item.Batteries)));
            pay.RegisteredInteractions.Add(new Display("You insert your coins into the machine, and a pack of batteries is dispensed!"));

            Interactions.Add(pay);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            if (!player.Inventory.GetItems().Any(x => x.ItemId.Equals(ItemId)))
            {
                var msg = new Display($"You are not carrying any {ItemId}!");
                msg.Do(player);
                return true;
            }

            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("use"))
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
