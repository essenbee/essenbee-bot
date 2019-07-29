using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Move : BaseAdventureCommand
    {
        public Move(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
            CheckEvents = true;
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var canMove = false;

            if (e.ArgsAsList.Count == 2)
            {
                var direction = e.ArgsAsList[1].ToLower();

                if (player.CurrentLocation.IsDark)
                {
                    if (!player.Statuses.Contains(PlayerStatus.HasLight))
                    {
                        if (DieRoller.Percentage(34))
                        {
                            player.ChatClient.PostDirectMessage(player, "You stumble around in the darkness and fall into " +
    "a deep pit. Your bones break as you thud into the rock at its base. Your lamp smashes and darkness engulfs you...");
                            player.Statuses.Add(PlayerStatus.IsDead);
                            _game.EndOfGame(player);
                            return;
                        }

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
                        if (MoveAffectedByEncumberedStatus(player, move))
                        {
                            return;
                        }

                        player.PriorLocation = player.CurrentLocation;
                        player.CurrentLocation = place;
                        player.Moves++;

                        if (player.Clocks != null && player.Clocks.Count > 0)
                        {
                            var keys = player.Clocks.Keys.ToList();

                            foreach (var key in keys)
                            {
                                var ticks = player.Clocks[key];
                                ticks++;
                                player.Clocks[key] = ticks;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(moveText))
                        {
                            player.ChatClient.PostDirectMessage(player, moveText);
                        }

                        player.ChatClient.PostDirectMessage(player, "*" + player.CurrentLocation.Name + "*");

                        return;
                    }
                }
            }

            player.ChatClient.PostDirectMessage(player, "You cannot go in that direction! Try *look* so see where you might go.");
            player.ChatClient.PostDirectMessage(player, "*" + player.CurrentLocation.Name + "*");
        }

        public bool MoveAffectedByEncumberedStatus(IAdventurePlayer player, IPlayerMove move)
        {
            if (player.Statuses.Contains(PlayerStatus.IsEncumbered))
            {
                player.ChatClient.PostDirectMessage(player, "You are currently encumbered.");

                if (move.Moves.Contains("up"))
                {
                    player.ChatClient.PostDirectMessage(player, "You are unable to move upward!");
                    player.ChatClient.PostDirectMessage(player, "*" + player.CurrentLocation.Name + "*");
                    return true;
                }

                if (move.Moves.Contains("down"))
                {
                    player.ChatClient.PostDirectMessage(player, "You slip and fall! All that weight drags you down. " +
                        "Your lamp smashes and darkness engulfs you...");
                    player.Statuses.Add(PlayerStatus.IsDead);
                    _game.EndOfGame(player);
                    return true;
                }
            }

            return false;
        }
    }
}
