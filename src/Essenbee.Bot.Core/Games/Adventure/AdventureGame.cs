using Essenbee.Bot.Core.Games.Adventure.Locations;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
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
        private readonly AdventureCommandRegistry _commandRegistry;
        private readonly Dictionary<Location, AdventureLocation> _locations;

        public AdventureGame()
        {
            _players = new List<AdventurePlayer>();
            _commandRegistry = new AdventureCommandRegistry(this);
            _locations = new ColossalCave().Build();
        }

        public void HandleCommand(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (IsNewPlayer(e))
            {
                var player = new AdventurePlayer {
                    Id = e.UserId,
                    UserName = e.UserName,
                    CurrentLocation = _locations[Location.Road],
                    Score = 0,
                    Moves = 0,
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

                    var command = _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch(cmd)) ??
                                  _commandRegistry.RegisteredCommands.FirstOrDefault(c => c.IsMatch("use"));

                    command?.Invoke(player, e);

                    if (player.Statuses.Contains(PlayerStatus.HasWon))
                    {
                        GameWon(player);
                    }

                    if (player.Statuses.Contains(PlayerStatus.IsDead))
                    {
                        GameLost(player);
                    }
                }
                else
                {
                    player.ChatClient.PostDirectMessage(player.Id, "What would you like me to do?");
                }
            }
        }

        private void GameLost(AdventurePlayer player)
        {
            
        }

        private void GameWon(AdventurePlayer player)
        {
            
        }

        public bool TryGetLocation(Location locationId, out AdventureLocation place)
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
            look?.Invoke(player, e);
        }

        private bool IsNewPlayer(ChatCommandEventArgs e) => _players.All(p => p.Id != e.UserId);

        private AdventurePlayer GetPlayer(string userId)
        {
            return _players.First(x => x.Id == userId);
        }
    }
}
