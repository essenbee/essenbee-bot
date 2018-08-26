using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Interfaces;
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

                    var command = _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch(cmd));

                    if (command is null)
                    {
                        command = _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch("use"));
                    }

                    command.Invoke(player, e);
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

            var look = _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch("look"));
            look.Invoke(player, e);
        }

        private bool IsNewPlayer(ChatCommandEventArgs e) => _players.All(p => p.Id != e.UserId);

        private AdventurePlayer GetPlayer(string userId)
        {
            return _players.First(x => x.Id == userId);
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

            var read = new ItemInteraction(this, "read");
            read.RegisteredInteractions.Add(new Display(whenRead.ToString()));
            leaflet.Interactions.Add(read);

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
                        {"south" , "valley" },
                    }
            };

            var bottle = new AdventureItem {
                ItemId = "bottle",
                Name = "small glass *bottle*",
                PluralName = "small glass *bottles*",
                IsPortable = true,
            };

            var shard = new AdventureItem {
                ItemId = "shard",
                Name = "*shard* of jagged glass",
                PluralName = "*shards* of jagged glass",
                IsPortable = true,
                IsEndlessSupply = false, // ToDo: how to handle endless items
            };

            var brokenGlass = new AdventureItem {
                ItemId = "glass",
                Name = "spread of broken glass",
                PluralName = "spread of broken glass",
                IsPortable = false,
                IsContainer = true,
                IsOpen = true,
                Contents = new List<AdventureItem> { shard },
            };

            var smash = new ItemInteraction(this, "smash", "break");
            smash.RegisteredInteractions.Add(new Display("You smash the bottle and glass flies everywhere!"));
            smash.RegisteredInteractions.Add(new RemoveFromInventory());
            smash.RegisteredInteractions.Add(new AddToLocation(brokenGlass));

            bottle.Interactions.Add(smash);

            var lamp = new AdventureItem {
                ItemId = "lamp",
                Name = "battered *lamp*",
                PluralName = "battered *lamps*",
                IsPortable = true,
                IsEndlessSupply = false, // ToDo: how to handle endless items
            };

            var light = new ItemInteraction(this, "light");
            light.RegisteredInteractions.Add(new ActivateItem("The lamp shines brightly."));
            light.RegisteredInteractions.Add(new AddPlayerStatus(PlayerStatus.HasLight));

            lamp.Interactions.Add(light);

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
                            IsPortable = true,
                        },
                        lamp,
                        bottle,
                        new AdventureItem
                        {
                            ItemId = "food",
                            Name = "packet of dried *food* rations",
                            PluralName = "packets of dried *food* rations",
                            IsPortable = true,
                            IsEndlessSupply = false, // ToDo: how to handle endless items
                        },
                },
                Moves = new Dictionary<string, string> {
                        {"west", "road" },
                        {"road", "road" },
                        {"out", "road" },
                        {"outside", "road" },
                }
            };

            var valley = new AdventureLocation {
                LocationId = "valley",
                Name = "Valley",
                ShortDescription = "in a valley",
                LongDescription = "in a valley in the forest beside a stream tumbling along a rocky bed.",
                Items = new List<AdventureItem>(),
                Moves = new Dictionary<string, string> {
                        {"north", "road" },
                        {"south", "slit" },
                }
            };

            var slit = new AdventureLocation {
                LocationId = "slit",
                Name = "Slit",
                ShortDescription = "at the slit in the streambed",
                LongDescription = "besides the stream. At your feet all the water of the stream splashes into a 2-inch slit in the rock. Downstream the streambed is bare rock.",
                Items = new List<AdventureItem>(),
                Moves = new Dictionary<string, string> {
                        {"north", "valley" },
                        {"south", "depression" },
                }
            };

            var grate = new AdventureItem {
                ItemId = "grate",
                Name = "strong steel grate",
                PluralName = "strong steel grates",
                IsPortable = false,
                IsOpen = false,
                IsLocked = true,
                ItemIdToUnlock = "key",
            };

            var open = new ItemInteraction(this, "open");
            open.RegisteredInteractions.Add(new Open());
            grate.Interactions.Add(open);

            var unlock = new ItemInteraction(this, "unlock");
            unlock.RegisteredInteractions.Add(new Unlock());
            unlock.RegisteredInteractions.Add(new Display("You open the grate and see a dark spce below it. A rusty iron ladder leads down into pitch blackness!"));
            grate.Interactions.Add(unlock);

            var depression = new AdventureLocation {
                LocationId = "depression",
                Name = "Depression",
                ShortDescription = "outside the grate",
                LongDescription = "in a 20-foot depression floored with bare dirt. Set into the dirt is a strong steel grate mounted in concrete. A dry streambed leads into the depression.",
                Items = new List<AdventureItem> {
                        grate,
                },
                Moves = new Dictionary<string, string> {
                        {"north", "slit" },
                }
            };

            dungeon.Add(0, startingLocation);
            dungeon.Add(1, building);
            dungeon.Add(2, valley);
            dungeon.Add(3, slit);
            dungeon.Add(4, depression);

            return dungeon;
        }
    }
}
