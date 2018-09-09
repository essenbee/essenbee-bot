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
        public IDungeon Dungeon { get; }

        private List<AdventurePlayer> _players;
        private readonly IAdventureCommandHandler _commandHandler;

        public AdventureGame()
        {
            _players = new List<AdventurePlayer>();
            _commandHandler = new CommandHandler(this);
            Dungeon = new Dungeon(this, new ColossalCave());
        }

        public AdventureGame(IDungeon dungeon)
        {
            _players = new List<AdventurePlayer>();
            _commandHandler = new CommandHandler(this);
            Dungeon = dungeon;
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

        private void JoinGame(IChatClient chatClient, ChatCommandEventArgs e)
        {
            var player = new AdventurePlayer(e.UserId, e.UserName, chatClient) { CurrentLocation = Dungeon.GetStartingLocation() };
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