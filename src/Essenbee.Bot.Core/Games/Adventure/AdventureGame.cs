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
        private readonly ICommandHandler _commandHandler;
        private readonly Dictionary<Location, IAdventureLocation> _locations;

        public AdventureGame()
        {
            _players = new List<AdventurePlayer>();
            _commandHandler = new CommandHandler(this);
            _locations = new ColossalCave().Build(this);
        }

        public void HandleCommand(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (IsNewPlayer(e))
            {
                JoinGame(chatClient, e);
            }
            else
            {
                var player = GetPlayer(e.UserId);

                if (e.ArgsAsList.Count > 0)
                {
                    _commandHandler.ExecutePlayerCommand(player, e);
                }
                else
                {
                    player.ChatClient.PostDirectMessage(player.Id, "What would you like me to do?");
                }
            }
        }

        public bool TryGetLocation(Location locationId, out IAdventureLocation place)
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

        private void JoinGame(IChatClient chatClient, ChatCommandEventArgs e)
        {
            var player = new AdventurePlayer(e.UserId, e.UserName, chatClient) { CurrentLocation = _locations[Location.Road] };
            _players.Add(player);
            chatClient.PostMessage(e.Channel, $"{e.UserName} has joined the Adventure!");

            var welcome = new StringBuilder("Welcome to Adventure!");
            welcome.AppendLine("Use `!adv help` to get some help.");
            player.ChatClient.PostDirectMessage(player.Id, welcome.ToString());

            var look = _commandHandler.GetCommand("look");
            look?.Invoke(player, e);
        }

        private bool IsNewPlayer(ChatCommandEventArgs e) => _players.All(p => p.Id != e.UserId);

        private AdventurePlayer GetPlayer(string userId)
        {
            return _players.First(x => x.Id == userId);
        }
    }
}