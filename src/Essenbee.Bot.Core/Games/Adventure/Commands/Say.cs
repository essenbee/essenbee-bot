using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Say : BaseAdventureCommand
    {
        public Say(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var otherPlayersHere = _game.Players.Where(p => (p.CurrentLocation.Name == player.CurrentLocation.Name) &&
                                                          (p.Id != player.Id)).ToList();

            var content = string.Join(" ", e.ArgsAsList.ToArray()).Trim();
            var message = $"{player.UserName} says: {content}";

            if (otherPlayersHere.Any())
            {
                foreach (var otherPlayer in otherPlayersHere)
                {
                    otherPlayer.ChatClient.PostDirectMessage(otherPlayer, message);
                }

                player.ChatClient.PostDirectMessage(player, $"You say: {content}");
            }
            else
            {
                player.ChatClient.PostDirectMessage(player, "Talking to yourself is the first sign of madness, y'know?");
            }
        }
    }
}
