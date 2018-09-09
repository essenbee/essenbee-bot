using System.Collections.Generic;
using System.Linq;
using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Rod : AdventureItem
    {
        internal Rod(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Rod;
            Name = "black *rod* with a rusty star on one end";
            PluralName = "black *rods* with rusty stars on the ends";
            IsActive = true;
            Contents = new List<IAdventureItem>();
            IsPortable = true;

            // Using the rod when at the fissure, creates a crystal bridge
            var use = new ItemInteraction(Game, "use", "wave");
            use.RegisteredInteractions.Add(new DisplayForLocation("You wave the rod with a flourish. The air shimmers and a beautiful crystal bridge appears, spanning the fissure to the west!",
                Location.FissureEast));
            use.RegisteredInteractions.Add(new DisplayForLocation("You wave the rod with a flourish.. The air shimmers and a beautiful crystal bridge appears, spanning the fissure to the east!!",
                Location.FissureWest));
            use.RegisteredInteractions.Add(new AddToLocation(ItemFactory.GetInstance(Game, Item.CrystalBridge)));

            use.RegisteredInteractions.Add(new AddMoves(new List<IPlayerMove>
            {
                new PlayerMove("You cautiously cross the crystal bridge...", Location.FissureWest, "west", "w", "bridge"),
            }, Game, Location.FissureEast));
            use.RegisteredInteractions.Add(new AddMoves(new List<IPlayerMove>
            {
                new PlayerMove("You step out onto the crystal bridge...", Location.FissureEast, "east", "e", "bridge"),
            }, Game, Location.FissureWest));

            Interactions.Add(use);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("use"))
                {
                    var success = Game.Dungeon.TryGetLocation(Location.FissureWest, out var fissureWest);

                    if (success)
                    {
                        interaction.RegisteredInteractions
                            .Add(new AddToLocation(ItemFactory.GetInstance(Game, Item.CrystalBridge), fissureWest));
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
