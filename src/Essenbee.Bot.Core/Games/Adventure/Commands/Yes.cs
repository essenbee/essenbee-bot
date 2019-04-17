using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Yes: BaseAdventureCommand
    {
        public Yes(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var didProcess = false;

            if (player.CurrentLocation.Items.Any())
            {
                var items = player.CurrentLocation.Items.ToList();

                foreach(var item in items)
                {
                    if (item.Interactions.Any(i => i.IsMatch("yes")))
                    {
                        didProcess = item.Interact("yes", player);
                    }
                }
            }
            
            if (!didProcess)
            {
                player.ChatClient.PostDirectMessage(player, "I'm not sure what you mean?");
            }
        }
    }
}
