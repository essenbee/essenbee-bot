using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Move : BaseAdventureCommand
    {
        public Move(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var canMove = false;
            var direction = e.ArgsAsList[1].ToLower();

            if (player.CurrentLocation.IsDark)
            {
                if (!player.Statuses.Contains(PlayerStatus.HasLight))
                {
                    player.ChatClient.PostDirectMessage(player, "It is pitch black! If you move around, you'll probably fall into a chasm or something...");
                }
            }

            if (player.CurrentLocation.ValidMoves.Any(d => d.IsMatch(direction)))
            {
                var move = player.CurrentLocation.ValidMoves.First(d => d.IsMatch(direction));
                var moveTo = move.Destination;
                var moveText = move.MoveText;

                canMove = _game.Dungeon.TryGetLocation(moveTo, out var place);

                if (canMove)
                {
                    if (player.Statuses.Contains(PlayerStatus.IsEncumbered))
                    {
                        player.ChatClient.PostDirectMessage(player, "You are currently encumbered.");

                        if (move.Moves.Contains("up"))
                        {
                            player.ChatClient.PostDirectMessage(player, "You are unable to move upward!");
                            player.ChatClient.PostDirectMessage(player, "*" + player.CurrentLocation.Name + "*");
                            return;
                        }

                        if (move.Moves.Contains("down") && player.CurrentLocation.IsDark)
                        {
                            // Player falls and is killed!
                        }
                    }

                    player.PriorLocation = player.CurrentLocation;
                    player.CurrentLocation = place;
                    player.Moves++;

                    if (!string.IsNullOrWhiteSpace(moveText))
                    {
                        player.ChatClient.PostDirectMessage(player, moveText);
                    }

                    player.ChatClient.PostDirectMessage(player, "*" + player.CurrentLocation.Name + "*");

                    return;
                }
            }

            player.ChatClient.PostDirectMessage(player, "You cannot go in that direction!");
            player.ChatClient.PostDirectMessage(player, "*" + player.CurrentLocation.Name + "*");
        }
    }
}
