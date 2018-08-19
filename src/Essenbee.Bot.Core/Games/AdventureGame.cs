using Essenbee.Bot.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Essenbee.Bot.Core.Games
{
    public class AdventureGame
    {

        private IDictionary<string, Action<ChatCommandEventArgs>> commands;
        private IList<AdventurePlayer> players;

        // For initial testing only
        private readonly Dictionary<int, AdventureLocation> locations = new Dictionary<int, AdventureLocation>
        {
            { 0, new AdventureLocation {
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
            commands = new Dictionary<string, Action<ChatCommandEventArgs>>();
        }

        public void HandleCommand(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (players.Any(x => x.UserName == e.UserName))
            {
                var player = GetPlayer(e.UserName);

                if (e.ArgsAsList.Count == 0)
                {
                    player.ChatClient.PostMessage(e.Channel, $"{e.UserName}, you are already playing Adventure!");
                }
                else
                {
                    var advCommands = e.ArgsAsList;

                    if (commands.ContainsKey(advCommands[0]))
                    {
                        commands[advCommands[0]](e);
                    }
                    else
                    {
                        player.ChatClient.PostMessage(e.Channel, $"Sorry {e.UserName}, I don't understand {advCommands[0]}.");
                    }
                }
            }
            else
            {
                if (e.ArgsAsList.Count == 0)
                {
                    var player = new AdventurePlayer {
                        UserName = e.UserName,
                        CurrentLocation = locations.First().Value,
                        Score = 0,
                        Moves = 1,
                        ChatClient = chatClient,
                    };

                    players.Add(player);

                    DisplayIntroText(player.ChatClient, e);
                }
                else
                {
                    chatClient.PostMessage(e.Channel, $"You are not playing Adventure, {e.UserName}. Use the command !adv to join the game.");
                }
            }
        }


        private AdventurePlayer GetPlayer(string userName)
        {
            return players.First(x => x.UserName == userName);
        }

        private void DisplayIntroText(IChatClient chatClient, ChatCommandEventArgs e)
        {
            chatClient.PostMessage(e.Channel, $"Welcome to Adventure, <{e.UserName}>!");
            AdvCommandLook(chatClient, e);
        }

        private void AdvCommandLook(IChatClient chatClient, ChatCommandEventArgs e)
        {
            var player = GetPlayer(e.UserName);

            var description = new StringBuilder(player.CurrentLocation.Name);
            description.AppendLine();
                        description.AppendLine($"<{player.UserName}>: You are {player.CurrentLocation.LongDescription}");

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

            chatClient.PostMessage(e.Channel, description.ToString());
        }
    }
}
