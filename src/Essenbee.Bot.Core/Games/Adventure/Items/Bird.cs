using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Bird : AdventureItem
    {
        internal Bird(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Bird;
            Name = "little *bird* singling cheerfully";
            PluralName = "little *birds* singing cheerfully";
            IsPortable = true;
            IsActive = true;
        }

        //public override bool Interact(string verb, AdventurePlayer player)
        //{
        //    verb = verb.ToLower();
        //    var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

        //    if (interaction != null)
        //    {
        //        if (interaction.Verbs.Contains("fill"))
        //        {
        //            if (!player.CurrentLocation.WaterPresent)
        //            {
        //                var msg = new Display("There is no water here!");
        //                msg.Do(player);
        //                return true;
        //            }

        //            if (!player.Inventory.GetItems().Any(x => x.ItemId.Equals(ItemId)))
        //            {
        //                var msg = new Display($"You are not carrying a {ItemId}!");
        //                msg.Do(player);
        //                return true;
        //            }

        //            if (Contents.Any(c => c.ItemId.Equals("water")))
        //            {
        //                var msg = new Display("The bottle is already full!");
        //                msg.Do(player);
        //                return true;
        //            }
        //        }

        //        foreach (var action in interaction.RegisteredInteractions)
        //        {
        //            action.Do(player, this);
        //        }

        //        return true;
        //    }

        //    return false;
        //}
    }
}
