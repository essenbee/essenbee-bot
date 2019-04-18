using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Dragon : AdventureItem
    {
        internal Dragon(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Dragon;
            Name = "huge fierce green dragon blocking your way! The dragon is sprawled out on an expensive-looking Persian rug lying";
            PluralName = "huge fierce green dragon blocking the way! The dragon is sprawled out on an expensive-looking Persian rug lying";
            IsPortable = false;

            var kill = new ItemInteraction(Game, "kill", "slay", "murder", "attack");
            kill.RegisteredInteractions.Add(new AddPlayerItemState("kill"));
            kill.RegisteredInteractions.Add(new Display("Do you just want to use your bare hands?"));
            Interactions.Add(kill);

            // Player says yes
            var yes = new ItemInteraction(Game, "yes");
            yes.RegisteredInteractions.Add(new RemovePlayerItemState("kill"));
            yes.RegisteredInteractions.Add(new Display("In an amazing feat of bravery, you kill the dragon with your bare hands!"));
            yes.RegisteredInteractions.Add(new RemoveFromLocation(this));
            yes.RegisteredInteractions.Add(new AddToLocation(ItemFactory.GetInstance(Game, Item.DeadDragon)));
            yes.RegisteredInteractions.Add(new RemoveDestination(Game, Location.SecretNorthEastCanyon));
            yes.RegisteredInteractions.Add(new AddMoves(new List<IPlayerMove>
                { new PlayerMove(string.Empty, Location.SecretNorthSouthCanyon, "north", "n") }, Game, Location.SecretNorthEastCanyon));
            Interactions.Add(yes);

            // Player says no
            var no = new ItemInteraction(Game, "no");
            no.RegisteredInteractions.Add(new RemovePlayerItemState("kill"));
            no.RegisteredInteractions.Add(new Display("I don't blame you!"));
            Interactions.Add(no);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if ((interaction.Verbs.Contains("yes") || interaction.Verbs.Contains("no"))
                    && !HasState(player, "kill"))
                {
                    return false;
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
