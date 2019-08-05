using Essenbee.Bot.Core.Games.Adventure.Events;
using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    internal class Lamp : AdventureItem
    {
        internal Lamp(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Lamp;
            Name = "battered *lamp*"; ;
            PluralName = "battered *lamps*";
            IsPortable = true;
            IsEndlessSupply = false;

            var light = new ItemInteraction(Game, "light");
            light.RegisteredInteractions.Add(new ActivateItem("The lamp shines brightly."));
            light.RegisteredInteractions.Add(new UpdateItemName("battered *lamp* which is lit"));
            light.RegisteredInteractions.Add(new AddPlayerStatus(PlayerStatus.HasLight));

            Interactions.Add(light);

            var extinguish = new ItemInteraction(Game, "extinguish", "ext");
            extinguish.RegisteredInteractions.Add(new DeactivateItem("The lamp turns off."));
            extinguish.RegisteredInteractions.Add(new UpdateItemName("battered *lamp*"));
            extinguish.RegisteredInteractions.Add(new RemovePlayerStatus(PlayerStatus.HasLight));

            Interactions.Add(extinguish);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            if (!player.Inventory.GetItems().Any(x => x.ItemId.Equals(ItemId)))
            {
                var msg = new Display($"You are not carrying a {ItemId}!");
                msg.Do(player);
                return true;
            }

            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("light"))
                {
                    if (IsActive)
                    {
                        var msg = new Display($"Your {ItemId} is already lit!");
                        msg.Do(player);
                        return true;
                    }

                    if (player.EventRecord.ContainsKey(EventIds.HasLamp) && 
                        player.EventRecord[EventIds.HasLamp] <= 0)
                    {
                        var msg = new Display($"Your {ItemId} is out of batteries!");
                        msg.Do(player);
                        return true;
                    }
                }

                if (interaction.Verbs.Contains("extinguish"))
                {
                    if (!IsActive)
                    {
                        var msg = new Display($"Your {ItemId} isn't lit already!");
                        msg.Do(player);
                        return true;
                    }
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
