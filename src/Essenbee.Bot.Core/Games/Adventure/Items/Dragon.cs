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
            Name = "huge green fierce dragon barring your way! The dragon is sprawled out on an expensive-looking Persian rug lying";
            PluralName = "huge green fierce dragon barring the way! The dragon is sprawled out on an expensive-looking Persian rug lying";
            IsPortable = false;

            var kill = new ItemInteraction(Game, "kill", "slay", "murder");
            kill.RegisteredInteractions.Add(new Display("Do you just want to use your bare hands?"));

            // TODO: get yes/no response from user

            kill.RegisteredInteractions.Add(new Display("In an amazing feat of bravery, you kill the dragon with your bare hands!"));
            kill.RegisteredInteractions.Add(new RemoveFromLocation(this));
            kill.RegisteredInteractions.Add(new AddToLocation(ItemFactory.GetInstance(Game, Item.DeadDragon)));
            kill.RegisteredInteractions.Add(new RemoveDestination(Game, Location.SecretNorthEastCanyon));
            kill.RegisteredInteractions.Add(new AddMoves(new List<IPlayerMove>
                { new PlayerMove(string.Empty, Location.SecretNorthSouthCanyon, "north", "n") }, Game, Location.SecretNorthEastCanyon));

            Interactions.Add(kill);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
                if (interaction.Verbs.Contains("kill"))
                {
                    foreach (var action in interaction.RegisteredInteractions)
                    {
                        action.Do(player, this);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
