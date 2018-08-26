using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
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

            var startingLocation = new Start(this);
            var building = new Building(this);
            var valley = new Valley(this);
            var slit = new Slit(this);
            var depression = new Depression(this);

            dungeon.Add(0, startingLocation);
            dungeon.Add(1, building);
            dungeon.Add(2, valley);
            dungeon.Add(3, slit);
            dungeon.Add(4, depression);

            return dungeon;
        }
    }
}
