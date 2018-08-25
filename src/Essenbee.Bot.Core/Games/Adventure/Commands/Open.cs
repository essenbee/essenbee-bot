using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Open : IAdventureCommand
    {
        private readonly IReadonlyAdventureGame _game;

        public Open(IReadonlyAdventureGame game)
        {
            _game = game;
        }

        public void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var location = player.CurrentLocation;
            var item = e.ArgsAsList[1].ToLower();

            var locationItem = location.Items.FirstOrDefault(i => i.Name == item || i.ItemId == item);

            if (locationItem is null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You cannot see a {item} here!");
                return;
            }

            if (locationItem.IsOpen)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item} is already open!");
                return;
            }

            if (locationItem.IsLocked)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"The {item} is locked!");
                return;
            }

            locationItem.IsOpen = true;
        }
    }
}
