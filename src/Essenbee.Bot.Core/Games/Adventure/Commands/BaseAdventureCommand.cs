using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public abstract class BaseAdventureCommand : IAdventureCommand
    {
        protected List<string> Verbs;
        protected readonly IReadonlyAdventureGame _game;

        public bool CheckEvents { get; set; }

        protected BaseAdventureCommand(IReadonlyAdventureGame game, params string[] verbs)
        {
            _game = game;
            Verbs = new List<string>(verbs);
        }

        public abstract void Invoke(IAdventurePlayer player, ChatCommandEventArgs e);

        public virtual bool IsMatch(string verb)
        {
            return Verbs.Any(v => verb.Equals(v));
        }
    }
}
