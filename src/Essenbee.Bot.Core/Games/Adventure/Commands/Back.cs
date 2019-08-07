using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Back : BaseAdventureCommand
    {

        public Back(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs) => CheckEvents = true;

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            if (player.CurrentLocation.NoBack)
            {
                player.ChatClient.PostDirectMessage(player, "Who knows which way is 'back' anymore?");
                return;
            }

            var backTo = player.PriorLocation;

            if (player.CurrentLocation.ValidMoves.Any(d => d.Destination.Equals(backTo.LocationId)))
            {
                player.PriorLocation = player.CurrentLocation;
                player.CurrentLocation = backTo;

                player.ChatClient.PostDirectMessage(player, "You make your way back from whence you came...");
            }
            else
            {
                player.ChatClient.PostDirectMessage(player, "There doesn't appear to be a way to can go back from here.");
            }
        }
    }
}
