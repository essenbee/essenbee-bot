using Essenbee.Bot.Core.Games.Adventure.Interactions;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class ItemInteraction
    {
        public List<string> Verbs;
        public readonly IReadonlyAdventureGame _game;
        public IList<IAction> RegisteredInteractions { get; set; }

        public ItemInteraction(IReadonlyAdventureGame game, params string[] verbs)
        {
            _game = game;
            Verbs = new List<string>(verbs);
            RegisteredInteractions = new List<IAction>();
        }
        
        public bool IsMatch(string verb)
        {
            return Verbs.Any(v => verb.Equals(v));
        }
    }
}
