using Essenbee.Bot.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

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
                            ItemId = "mailbox",
                            Name = "small mailbox",
                            IsOpen = true,
                            IsContainer = true,
                            Contents = new List<AdventureItem>
                            {
                                new AdventureItem
                                {
                                    ItemId = "leaflet",
                                    Name = "*leaflet*",
                                    CanBeTaken = true
                                }
                            }
                        }
                    },
                    Moves = new Dictionary<string, string>
                    {
                        {"east", "building" },
                        {"enter", "building" },
                        {"in", "building" },
                        {"inside", "building" },
                        {"building", "building" },
                    }
                }
            },
            { 1, new AdventureLocation {
                LocationId = "building",
                Name = "Small Brick Building",
                ShortDescription = "inside a small brick building.",
                LongDescription = " inside a small brick building, a well house for a bubbling spring.",
                Items = new List<AdventureItem>
                {
                    new AdventureItem
                        {
                            ItemId = "key",
                            Name = "large iron *key*",
                            CanBeTaken = true
                        },
                        new AdventureItem
                        {
                            ItemId = "lamp",
                            Name = "battered *lamp*",
                            CanBeTaken = true,
                        },
                        new AdventureItem
                        {
                            ItemId = "bottle",
                            Name = "small glass *bottle*",
                            CanBeTaken = true,
                        },
                        new AdventureItem
                        {
                            ItemId = "food",
                            Name = "parcel of *food* items",
                            CanBeTaken = true,
                        },
                },
                Moves = new Dictionary<string, string> {
                        {"west", "road" },
                        {"road", "road" },
                        {"out", "road" },
                        {"outside", "road" },
                }
            }
            }
        };

        public AdventureGame()
        {
            players = new List<AdventurePlayer>();
            //var json = JsonConvert.SerializeObject(locations);
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
                    var cmd = advCommands[0].ToLower();

                    if (commands.ContainsKey(cmd))
                    {
                        commands[cmd](player, e);
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
                {"help", AdvCommandHelp },
                {"look", AdvCommandLook},
                { "l", AdvCommandLook},
                { "go", AdvCommandMove},
                { "move", AdvCommandMove},
                { "take", AdvCommandTake},
                { "get", AdvCommandTake},
                { "inventory", AdvCommandInventory},
                { "inv", AdvCommandInventory},
                { "open", AdvCommandOpen},
                { "drop", AdvCommandDrop},
            };
        }

        private AdventurePlayer GetPlayer(string userId)
        {
            return players.First(x => x.Id == userId);
        }

        private bool TryGetLocation(string locationId, out AdventureLocation place)
        {
            var location = locations.Where(l => l.Value.LocationId.Equals(locationId)).ToList();
            place = null;

            if (location.Count == 0)
            {
                return false;
            }

            place = location[0].Value;

            return true;
        }

        private void DisplayIntroText(AdventurePlayer player, ChatCommandEventArgs e)
        {
            player.ChatClient.PostDirectMessage(player.Id, $"Welcome to Adventure!");
            AdvCommandLook(player, e);
        }

        private void AdvCommandHelp(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var helpText = new StringBuilder("I know several commands to aid you in your exploration, such as:");
            helpText.AppendLine();
            helpText.AppendLine("\t'look'");
            helpText.AppendLine("\t'go'");
            helpText.AppendLine("\t'take'");
            helpText.AppendLine("\t'use'");
            helpText.AppendLine("\t'inventory'");
            helpText.AppendLine();
            helpText.AppendLine("Some of the places you will visit have items lying around. If such an item is shown in *bold* text, you can take it and carry it with you; it may or may not be of any use.");
            player.ChatClient.PostDirectMessage(player.Id, helpText.ToString());
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
                        description.AppendLine($"\tA {content.Name}");
                    }
                }
            }

            player.ChatClient.PostDirectMessage(player.Id, description.ToString());
        }

        private void AdvCommandMove(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var canMove = false;
            var direction = e.ArgsAsList[1].ToLower();

            if (player.CurrentLocation.Moves.ContainsKey(direction))
            {
                var moveTo = player.CurrentLocation.Moves[direction];
                canMove = TryGetLocation(moveTo, out var place);
                player.CurrentLocation = place;
            }

            if (canMove)
            {
                player.ChatClient.PostDirectMessage(player.Id, "*" + player.CurrentLocation.Name + "*");
            }
            else
            {
                player.ChatClient.PostDirectMessage(player.Id, "You cannot go in that direction!");
            }
        }

        private void AdvCommandTake(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var location = player.CurrentLocation;
            var item = e.ArgsAsList[1].ToLower();

            var locationItem = location.Items.FirstOrDefault(i => i.Name == item || i.ItemId == item);
            var containers = location.Items.Where(i => i.IsContainer && i.Contents.Count > 0).ToList();

            foreach (var container in containers)
            {
                foreach (var containedItem in container.Contents)
                {
                    if (containedItem.ItemId == item && containedItem.CanBeTaken)
                    {
                        player.Inventory.Add(containedItem);
                        container.Contents.Remove(containedItem);
                        player.ChatClient.PostDirectMessage(player.Id, $"You are now carrying a {item} with you.");

                        return;
                    }
                }
            }

            if (locationItem is null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You cannot see a {item} here!");
                return;
            }

            if (!locationItem.CanBeTaken)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You cannot carry a {item} with you!");
                return;
            }

            player.Inventory.Add(locationItem);
            player.CurrentLocation.Items.Remove(locationItem);
            player.ChatClient.PostDirectMessage(player.Id, $"You are now carrying a {item} with you.");
        }

        private void AdvCommandDrop(AdventurePlayer player, ChatCommandEventArgs e)
        {
            var args = e.ArgsAsList;

            if (args.Count == 1)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"What wpoudl you like to drop?");
                return;
            }

            var itemToDrop = args[1];
            var itemInInventory = player.Inventory.FirstOrDefault(x => x.Name.Equals(itemToDrop, StringComparison.InvariantCultureIgnoreCase));

            if (itemInInventory == null)
            {
                player.ChatClient.PostDirectMessage(player.Id, $"You don't have a {itemToDrop} to drop!");
            }
            else
            {
                player.Inventory.Remove(itemInInventory);
                player.CurrentLocation.Items.Add(itemInInventory);
                player.ChatClient.PostDirectMessage(player.Id, $"You dropped a {itemToDrop}");
            }
        }

        private void AdvCommandInventory(AdventurePlayer player, ChatCommandEventArgs e)
        {
            if (player.Inventory.Count == 0)
            {
                player.ChatClient.PostDirectMessage(player.Id, "You are not carrying anything with you at the moment.");
                return;
            }

            var inventory = new StringBuilder("You are carrying these items with you:");
            inventory.AppendLine();

            foreach (var item in player.Inventory)
            {
                inventory.AppendLine($"\ta {item.Name}");
            }

            player.ChatClient.PostDirectMessage(player.Id, inventory.ToString());
        }

        private void AdvCommandOpen(AdventurePlayer player, ChatCommandEventArgs e)
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
