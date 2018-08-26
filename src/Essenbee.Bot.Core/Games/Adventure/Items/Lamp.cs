using Essenbee.Bot.Core.Games.Adventure.Interactions;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    internal class Lamp : AdventureItem
    {
        internal Lamp(IReadonlyAdventureGame game) : base(game)
        {
            ItemId = "lamp";
            Name = "battered *lamp*";
            PluralName = "battered *lamps*";
            IsPortable = true;
            IsEndlessSupply = true;

            var light = new ItemInteraction(Game, "light");
            light.RegisteredInteractions.Add(new ActivateItem("The lamp shines brightly."));
            light.RegisteredInteractions.Add(new AddPlayerStatus(PlayerStatus.HasLight));

            Interactions.Add(light);
        }

        public override bool Interact(string verb, AdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("light"))
                {
                    if (!player.Inventory.GetItems().Any(x => x.ItemId.Equals(ItemId)))
                    {
                        var msg = new Display($"You are not carrying a {ItemId}!");
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
