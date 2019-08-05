using Essenbee.Bot.Core.Games.Adventure.Events;
using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Batteries : AdventureItem
    {
        internal Batteries(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Batteries;
            Name = "packet of fresh *batteries*";
            PluralName = "packets of fresh *batteries";
            IsPortable = true;
            IsEndlessSupply = false;

            var installBatteries = new ItemInteraction(Game, "use", "install", "fit", "renew", "refresh");
            installBatteries.RegisteredInteractions.Add(new RemoveFromInventory());
            installBatteries.RegisteredInteractions.Add(new Display("You fit the fresh batteries into your lamp!"));
            installBatteries.RegisteredInteractions.Add(new ActivateItem("The lamp shines brightly."));
            installBatteries.RegisteredInteractions.Add(new UpdateItemName("battered *lamp* which is lit"));
            installBatteries.RegisteredInteractions.Add(new AddPlayerStatus(PlayerStatus.HasLight));
            installBatteries.RegisteredInteractions.Add(new AddToLocation(ItemFactory.GetInstance(Game, Item.SpentBatteries)));
            installBatteries.RegisteredInteractions.Add(new UpdateEventRecord(EventIds.HasLamp, 2000));

            Interactions.Add(installBatteries);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());
            var lamp = player.Inventory.GetItems().FirstOrDefault(x => x.ItemId == Item.Lamp);

            if (interaction != null && lamp != null)
            {
                foreach (var action in interaction.RegisteredInteractions)
                {
                    action.Do(player, lamp);
                }

                return true;

            }

            return false;
        }
    }
}


