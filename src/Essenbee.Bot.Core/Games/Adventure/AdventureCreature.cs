using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureCreature : AdventureItem
    {
        protected AdventureCreature(IReadonlyAdventureGame game, string[] nouns) : base(game, nouns)
        {
            IsCreature = true;

            // Player says no to attacking with bare hands...
            var no = new ItemInteraction(Game, "no");
            no.RegisteredInteractions.Add(new RemovePlayerItemState("attack"));
            no.RegisteredInteractions.Add(new Display("I don't blame you!"));
            Interactions.Add(no);
        }
    }
}
