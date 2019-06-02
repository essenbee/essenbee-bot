using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class LittleAxe : AdventureItem
    {
        internal LittleAxe(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.LittleAxe;
            Name = "a small *axe*";
            PluralName = "small *axes*";
            IsPortable = true;
            IsWeapon = true;

            var use = new ItemInteraction(Game, "use");
            use.RegisteredInteractions.Add(new Display("You attack the dwarf with the little axe!!"));
            Interactions.Add(use);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                var dwarf = GetDwarfIfPresent(player);

                if (dwarf != null)
                {
                    foreach (var action in interaction.RegisteredInteractions)
                    {
                        action.Do(player, this);
                    }

                    return true;
                }

                player.ChatClient.PostDirectMessage(player, 
                    "The axe doesn't seem to be of much use in this particular situation!");
            }

            return false;
        }

        private IAdventureItem GetDwarfIfPresent(IAdventurePlayer player) =>
            player.CurrentLocation.Items.FirstOrDefault(i => i.ItemId.Equals(Item.Dwarf));
    }
}
