using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class ShadowyFigure : AdventureItem
    {
        internal ShadowyFigure(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.ShadowyFigure;
            Name = "shadowy *figure* peering at you through the window";
            PluralName = "shadowy *figures* peering at you through the window";
            IsPortable = false;

            var wave = new ItemInteraction(Game, "wave");
            wave.RegisteredInteractions.Add(new Display("The shadowy figure waves back at you. Is it trying to get your attention?"));
            Interactions.Add(wave);
        }

        public override bool Interact(string verb, IAdventurePlayer player)
        {
            verb = verb.ToLower();
            var interaction = Interactions.FirstOrDefault(c => c.IsMatch(verb) && c.ShouldExecute());

            if (interaction != null)
            {
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
