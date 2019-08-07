using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Linq;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Look : BaseAdventureCommand
    {
        public Look (IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
            CheckEvents = false;
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
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
                    if (item != null)
                    {
                        // ToDo: need to devise a correct solution for each player only getting one lamp in multi-player
                        //if (item.ItemId.Equals(Item.Lamp) && 
                        //    player.CurrentLocation.LocationId.Equals(Location.Building) && 
                        //    player.EventRecord.ContainsKey(Events.EventIds.HasLamp))
                        //{
                        //    continue;
                        //}

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
            }
            else
            {
                description.AppendLine();
                description.AppendLine("It is pitch black, you cannot see a thing!");
            }

            player.ChatClient.PostDirectMessage(player, description.ToString());
        }
    }
}
