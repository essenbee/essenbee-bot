using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Move : BaseAdventureCommand
    {
        public Move(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var canMove = false;
            var direction = e.ArgsAsList[1].ToLower();

            if (player.CurrentLocation.IsDark)
            {
                if (!player.Statuses.Contains(PlayerStatus.HasLight))
                {
                    player.ChatClient.PostDirectMessage(player.Id, "It is pitch black! If you move around, you'll probably fall into a chasm or something...");
                }
            }

            if (player.CurrentLocation.ValidMoves.Any(d => d.IsMatch(direction)))
            {
                var moveTo = player.CurrentLocation.ValidMoves.First(d => d.IsMatch(direction)).Destination;
                var moveText = player.CurrentLocation.ValidMoves.First(d => d.IsMatch(direction)).MoveText;

                canMove = _game.TryGetLocation(moveTo, out var place);

                if (canMove)
                {
                    player.CurrentLocation = place;
                    player.Moves++;

                    if (!string.IsNullOrWhiteSpace(moveText))
                    {
                        player.ChatClient.PostDirectMessage(player.Id, moveText);
                    }

                    player.ChatClient.PostDirectMessage(player.Id, "*" + player.CurrentLocation.Name + "*");

                    return;
                }
            }

            player.ChatClient.PostDirectMessage(player.Id, "You cannot go in that direction!");
        }
    }
}
