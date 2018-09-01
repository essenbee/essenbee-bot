using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class PintOfWater : AdventureItem
    {
        internal PintOfWater(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.PintOfWater;
            Name = "pint of *water*";
            PluralName = "pints of *water*";
            IsPortable = false;
            IsEndlessSupply = true;
            MustBeContainedIn = Item.Bottle;

            var drink = new ItemInteraction(Game, "drink");
            Interactions.Add(drink);
        }

        public override bool Interact(string verb, AdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("drink"))
                {
                    if (!player.CurrentLocation.WaterPresent)
                    {
                        if (player.Inventory.GetItems().Any(i => i.ItemId.Equals(Item.Bottle)))
                        {
                            var bottle = player.Inventory.GetItems().First(i => i.ItemId.Equals(Item.Bottle));
                            if (bottle.Contents.Any(w => w.ItemId.Equals(Item.PintOfWater)))
                            {
                                var water = bottle.Contents.Where(i => i.ItemId.Equals(Item.PintOfWater));
                                interaction.RegisteredInteractions.Add(new RemoveFromInventory());
                            }
                        }
                        else
                        {
                            var msg = new Display("There is no water here!");
                            msg.Do(player);
                            return true;
                        }
                    }

                    interaction.RegisteredInteractions.Add(new Display("Mmmm, refreshing!"));
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
