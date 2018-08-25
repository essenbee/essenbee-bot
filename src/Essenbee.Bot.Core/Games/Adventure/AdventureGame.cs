using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventureGame : IGame, IReadonlyAdventureGame
    {
        public ReadOnlyCollection<AdventurePlayer> Players => _players.AsReadOnly();

        private List<AdventurePlayer> _players;
        private AdventureCommandRegistry _commandRegistry;
        private readonly Dictionary<int, AdventureLocation> _locations = new Dictionary<int, AdventureLocation>();

        public AdventureGame()
        {
            _players = new List<AdventurePlayer>();
            _commandRegistry = new AdventureCommandRegistry(this);
            _locations = BuildDungeon();
        }

        public void HandleCommand(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (IsNewPlayer(e))
            {
                var player = new AdventurePlayer {
                    Id = e.UserId,
                    UserName = e.UserName,
                    CurrentLocation = _locations.First().Value,
                    Score = 0,
                    Moves = 1,
                    ChatClient = chatClient,
                };

                _players.Add(player);
                chatClient.PostMessage(e.Channel, $"{e.UserName} has joined the Adventure!");

                DisplayIntroText(player, e);
            }
            else
            {
                var player = GetPlayer(e.UserId);

                if (e.ArgsAsList.Count > 0)
                {
                    var advCommands = e.ArgsAsList;
                    var cmd = advCommands[0].ToLower();

                    if (_commandRegistry.RegisteredCommands.ContainsKey(cmd))
                    {
                        _commandRegistry.RegisteredCommands[cmd].Invoke(player, e);
                    }
                    else
                    {
                        player.ChatClient.PostDirectMessage(player.Id, $"Sorry, I don't understand {advCommands[0]}.");
                    }
                }
                else
                {
                    player.ChatClient.PostDirectMessage(player.Id, "What would you like me to do?");
                }
            }
        }

        public bool TryGetLocation(string locationId, out AdventureLocation place)
        {
            var location = _locations.Where(l => l.Value.LocationId.Equals(locationId)).ToList();
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
            var welcome = new StringBuilder("Welcome to Adventure!");
            welcome.AppendLine("Use `!adv help` to get some help.");
            player.ChatClient.PostDirectMessage(player.Id, welcome.ToString());
            _commandRegistry.RegisteredCommands["look"].Invoke(player, e);
        }

        private bool IsNewPlayer(ChatCommandEventArgs e) => _players.All(p => p.Id != e.UserId);

        private AdventurePlayer GetPlayer(string userId)
        {
            return _players.First(x => x.Id == userId);
        }

        private static void ReadLeaflet(AdventurePlayer player)
        {
            var msg = new StringBuilder("You read the leaflet and this is what it says:");
            msg.AppendLine();
            msg.AppendLine("Somewhere nearby lies the fabled Colossal Cave, a place of danger, mystery and, some say, magic.");

            player.ChatClient.PostDirectMessage(player.Id, msg.ToString());
        }

        // Temporary method - need to build this from stored data
        private Dictionary<int, AdventureLocation> BuildDungeon()
        {
            var dungeon = new Dictionary<int, AdventureLocation>();

            var leaflet = new AdventureItem {
                ItemId = "leaflet",
                Name = "*leaflet*",
                PluralName = "*leaflets*",
                IsPortable = true,
            };

            var whenRead = new StringBuilder("You read the leaflet and this is what it says:");
            whenRead.AppendLine();
            whenRead.AppendLine("Somewhere nearby lies the fabled Colossal Cave, a place of danger, mystery and, some say, magic.");
            leaflet.AddInteraction("read", new Display(whenRead.ToString()));

            var mailbox = new AdventureItem {
                ItemId = "mailbox",
                Name = "small mailbox",
                PluralName = "small mailboxes",
                IsOpen = true,
                IsContainer = true,
                Contents = new List<AdventureItem> { leaflet }
            };

            var startingLocation = new AdventureLocation {
                LocationId = "road",
                Name = "End of a Road",
                ShortDescription = "standing at the end of a road.",
                LongDescription = "standing at the end of a road before a small brick building. Around you is a forest.  A small stream flows out of the building and down a gully.",
                Items = new List<AdventureItem> { mailbox },
                Moves = new Dictionary<string, string>
                        {
                        {"east", "building" },
                        {"enter", "building" },
                        {"in", "building" },
                        {"inside", "building" },
                        {"building", "building" },
                    }
            };

            var bottle = new AdventureItem {
                ItemId = "bottle",
                Name = "small glass *bottle*",
                PluralName = "small glass *bottles*",
                IsPortable = true,
                InteractionAlias = new Dictionary<string, string> {
                    { "break", "smash" }
                },
            };

            var brokenGlass = new AdventureItem {
                ItemId = "glass",
                Name = "lot of broken glass",
                PluralName = "lot of broken glass",
                IsPortable = false,
            };

            bottle.AddInteraction("smash", new Display("You smash the bottle and glass flies everywhere!"));
            bottle.AddInteraction("smash", new RemoveFromInventory());
            bottle.AddInteraction("smash", new AddToLocation(brokenGlass));

            var building = new AdventureLocation {
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
                            PluralName = "large iron *keys*",
                            IsPortable = true
                        },
                        new AdventureItem
                        {
                            ItemId = "lamp",
                            Name = "battered *lamp*",
                            PluralName = "battered *lamps*",
                            IsPortable = true,
                            IsEndlessSupply = true,
                        },
                        bottle,
                        new AdventureItem
                        {
                            ItemId = "food",
                            Name = "packet of dried *food* rations",
                            PluralName = "packets of dried *food* rations",
                            IsPortable = true,
                            IsEndlessSupply = true,
                        },
                },
                Moves = new Dictionary<string, string> {
                        {"west", "road" },
                        {"road", "road" },
                        {"out", "road" },
                        {"outside", "road" },
                }
            };

            dungeon.Add(0, startingLocation);
            dungeon.Add(1, building);

            return dungeon;
        }
    }
}
