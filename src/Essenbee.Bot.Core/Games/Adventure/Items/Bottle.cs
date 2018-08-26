using Essenbee.Bot.Core.Games.Adventure.Interactions;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Bottle : AdventureItem
    {
        public Bottle(IReadonlyAdventureGame game) : base(game)
        {
            ItemId = "bottle";
            Name = "small glass *bottle*";
            PluralName = "small glass *bottles*";
            IsContainer = true;
            IsPortable = true;

            var smash = new ItemInteraction(Game, "smash", "break");
            smash.RegisteredInteractions.Add(new Display("You smash the bottle and glass flies everywhere!"));
            smash.RegisteredInteractions.Add(new RemoveFromInventory());
            smash.RegisteredInteractions.Add(new AddToLocation(new BrokenGlass(Game)));

            Interactions.Add(smash);

            var fill = new ItemInteraction(Game, "fill");
            fill.RegisteredInteractions.Add(new Display("You reach down and fill the bottle with water."));
            fill.RegisteredInteractions.Add(new AddToItemContents(new PintOfWater(Game)));
        }

        public override bool Interact(string verb, AdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("fill"))
                {
                    if (!player.CurrentLocation.WaterPresent)
                    {
                        var msg = new Display("There is no water here!");
                        msg.Do(player);
                        return false;
                    }

                    if (Contents.Any(c => c.ItemId.Equals("water")))
                    {
                        var msg = new Display("The bottl is already full!");
                        msg.Do(player);
                        return false;
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
