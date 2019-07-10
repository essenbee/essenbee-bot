using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class No : BaseAdventureCommand
    {
        public No(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
            CheckEvents = false;
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var didProcess = false;

            if (player.CurrentLocation.Items.Any())
            {
                var items = player.CurrentLocation.Items.ToList();

                foreach (var item in items)
                {
                    if (item.Interactions.Any(i => i.IsMatch("no")))
                    {
                        didProcess = item.Interact("no", player);
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
