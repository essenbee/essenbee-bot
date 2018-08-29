using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Look : BaseAdventureCommand
    {
        public Look (IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var canSee = true;
            var description = new StringBuilder("*" + player.CurrentLocation.Name + "*");

            if (player.CurrentLocation.IsDark)
            {
                if (!player.Statuses.Contains(PlayerStatus.HasLight))
                {
                    canSee = false;
                }
            }

            if (canSee)
            {
                description.AppendLine();
                description.AppendLine($"You are {player.CurrentLocation.LongDescription}");

                var otherPlayersHere = _game.Players.Where(p => p.CurrentLocation.Name == player.CurrentLocation.Name &&
                                                          p.Id != player.Id).ToList();

                if (otherPlayersHere.Any())
                {
                    description.AppendLine();
                    foreach (var otherPlayer in otherPlayersHere)
                    {
                        description.AppendLine($"\t{otherPlayer.UserName} is also here.");
                    }
                }

                description.AppendLine();

                foreach (var item in player.CurrentLocation.Items)
                {
                    description.AppendLine(item.IsEndlessSupply
                        ? $"There are several {item.PluralName} here."
                        : $"There is a {item.Name} here.");

                    if (item.Contents.Any() && (item.IsOpen || item.IsTransparent))
                    {
                        description.AppendLine($"The {item.Name} contains:");

                        foreach (var content in item.Contents)
                        {
                            description.AppendLine($"\tA {content.Name}");
                        }
                    }
                }
            }
            else
            {
                description.AppendLine();
                description.AppendLine("It is pitch black, you cannot see a thing!");
            }

            player.ChatClient.PostDirectMessage(player.Id, description.ToString());
        }
    }
}
