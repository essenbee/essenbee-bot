using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    class AliasCommand : IAction
    {
        private IAdventureCommand _command;

        public AliasCommand(IAdventureCommand command) => _command = command;

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            var args = new List<string> 
            {
                nameof(_command),
                item.Nouns.First()
            };
            var e = new ChatCommandEventArgs(nameof(_command), args,
                string.Empty, player.Id, player.UserName, string.Empty);
            _command.Invoke(player, e);

            return true;
        }
    }
}
