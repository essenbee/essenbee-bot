using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Creatures
{
    public class Dragon : AdventureCreature
    {
        internal Dragon(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Dragon;
            Name = "huge fierce green dragon blocking your way! The dragon is sprawled out on an expensive-looking Persian rug lying";
            PluralName = "huge fierce green dragon blocking the way! The dragon is sprawled out on an expensive-looking Persian rug lying";
            IsPortable = false;

            // Player says yes to attacking with bare hands...
            var yes = new ItemInteraction(Game, "yes");
            yes.RegisteredInteractions.Add(new RemovePlayerItemState("attack"));
            yes.RegisteredInteractions.Add(new Display("Congratulations! You have just vanquished a dragon with your bare " +
                "hands! (Unbelievable, isn't it?)"));
            yes.RegisteredInteractions.Add(new RemoveFromLocation(this));
            yes.RegisteredInteractions.Add(new AddToLocation(ItemFactory.GetInstance(Game, Item.DeadDragon)));
            yes.RegisteredInteractions.Add(new StartClock("dragon"));
            yes.RegisteredInteractions.Add(new RemoveDestination(Game, Location.SecretNorthEastCanyon));
            yes.RegisteredInteractions.Add(new AddMoves(new List<IPlayerMove>
                { new PlayerMove(string.Empty, Location.SecretNorthSouthCanyon, "north", "n") }, Game, Location.SecretNorthEastCanyon));
            Interactions.Add(yes);
        }

        public override void Attack(IAdventurePlayer player, IAdventureItem troll, IAdventureItem weapon)
        {
            if (weapon != null)
            {
                player.ChatClient.PostDirectMessage(player, $"You try to to attack the dragon, but it breathes acid at your {weapon.Name}!");
                player.ChatClient.PostDirectMessage(player, $"You drop the ruined {weapon.Name} and it quickly dissolves into a puddle of slag!");
                var loseWeapon = new RemoveFromInventory();
                loseWeapon.Do(player, weapon);
            }

            return;
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if ((interaction.Verbs.Contains("yes") || interaction.Verbs.Contains("no"))
                    && !HasState(player, "attack"))
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
