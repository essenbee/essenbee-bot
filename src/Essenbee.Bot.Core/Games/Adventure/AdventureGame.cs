using Essenbee.Bot.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureGame
    {

        private IDictionary<string, Action<AdventurePlayer, ChatCommandEventArgs>> commands;
        private IList<AdventurePlayer> players;

        // For initial testing only
        private readonly Dictionary<int, AdventureLocation> locations = new Dictionary<int, AdventureLocation>
        {
            { 0, new AdventureLocation {
                    LocationId = "road",
                    Name = "End of a Road",
                    ShortDescription = "standing at the end of a road.",
                    LongDescription = "standing at the end of a road before a small brick building. Around you is a forest.  A small stream flows out of the building and down a gully.",
                    Items = new List<AdventureItem>
                    {
                        new AdventureItem
                        {
                            Name = "small mailbox",
                            IsOpen = true,
                            Contents = new List<AdventureItem>
                            {
                                new AdventureItem
                                {
                                    Name = "A leaflet"
                                }
                            }
                        }
                    }
                }
            }
        };

        public AdventureGame()
        {
            players = new List<AdventurePlayer>();
            InitialiseCommands();
        }

        public void HandleCommand(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (players.Any(x => x.Id == e.UserId))
            {
                var player = GetPlayer(e.UserId);

                if (e.ArgsAsList.Count == 0)
                {
                    player.ChatClient.PostDirectMessage(player.Id, $"{e.UserName}, you are already playing Adventure!");
                }
                else
                {
                    var advCommands = e.ArgsAsList;

                    if (commands.ContainsKey(advCommands[0]))
                    {
                        commands[advCommands[0]](player, e);
                    }
                    else
                    {
                        player.ChatClient.PostDirectMessage(player.Id, $"Sorry, I don't understand {advCommands[0]}.");
                    }
                }
            }
            else
            {
                if (e.ArgsAsList.Count == 0)
                {
                    var player = new AdventurePlayer {
                        Id = e.UserId,
                        UserName = e.UserName,
                        CurrentLocation = locations.First().Value,
                        Score = 0,
                        Moves = 1,
                        ChatClient = chatClient,
                    };

                    players.Add(player);
                    chatClient.PostMessage(e.Channel, $"{e.UserName} has joined the Adventure!");

                    DisplayIntroText(player, e);
                }
                else
                {
                    chatClient.PostDirectMessage(e.UserId, $"You are not playing Adventure. Use the command !adv to join the game.");
                }
            }
        }

        private void InitialiseCommands()
        {
            commands = new Dictionary<string, Action<AdventurePlayer, ChatCommandEventArgs>>
            {
                // {"help", AdvCommandHelp },
                {"look", AdvCommandLook},
                { "l", AdvCommandLook}
            };
        }

        private AdventurePlayer GetPlayer(string userId)
        {
            return players.First(x => x.Id == userId);
        }

        private void DisplayIntroText(AdventurePlayer player, ChatCommandEventArgs e)
        {
            player.ChatClient.PostDirectMessage(player.Id, $"Welcome to Adventure!");
            AdvCommandLook(player, e);
        }

        private void AdvCommandLook(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var description = new StringBuilder("*" + player.CurrentLocation.Name + "*");
            description.AppendLine();
            description.AppendLine($"You are {player.CurrentLocation.LongDescription}");

            var otherPlayersHere = players.Where(p => p.CurrentLocation.Name == player.CurrentLocation.Name &&
                                                      p.UserName != player.UserName);

            if (otherPlayersHere.Count() > 0)
            {
                description.AppendLine();
            }

            foreach (var otherPlayer in otherPlayersHere)
            {
                description.AppendLine($"\t{otherPlayer.UserName} is also here.");
            }

            description.AppendLine();

            foreach (var item in player.CurrentLocation.Items)
            {
                description.AppendLine($"There is a {item.Name} here.");

                if (item.Contents.Any() && item.IsOpen)
                {
                    description.AppendLine($"The {item.Name} contains:");

                    foreach (var content in item.Contents)
                    {
                        description.AppendLine($"\t{content.Name}");
                    }
                }
            }

            player.ChatClient.PostDirectMessage(player.Id, description.ToString());
        }
    }
}
