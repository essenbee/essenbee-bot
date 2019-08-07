using Essenbee.Bot.Core.Games.Adventure.Events;
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
        public List<IMonsterManager> MonsterManagers { get; } = new List<IMonsterManager>();

        private List<AdventurePlayer> _players;
        private readonly IAdventureCommandHandler _commandHandler;

        public AdventureGame()
        {
            _players = new List<AdventurePlayer>();
            _commandHandler = new CommandHandler(this);
            Dungeon = new Dungeon(this, new ColossalCave());
            MonsterManagers.Add(new DwarfManager(Dungeon.Locations));
            MonsterManagers.Add(new PirateManager(Dungeon.Locations));
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
                    var checkForEvents = _commandHandler.ExecutePlayerCommand(player, e);

                    if (checkForEvents)
                    {
                        EventManager.CheckForEvents(player);
                    }
                }
                else
                {
                    player.ChatClient.PostDirectMessage((chatClient.UseUsernameForIM) ? player.UserName : player.Id, "What would you like me to do?");
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

        public void EndOfGame(IAdventurePlayer player)
        {
            var thePlayer = player as AdventurePlayer;

            if (player.Statuses.Contains(PlayerStatus.IsDead))
            {
                player.ChatClient.PostDirectMessage(player, "You have been killed, another victim of the perils of Colossal Cave.");
            }

            var found = Dungeon.TryGetLocation(Locations.Location.Building, out var building);

            if (found)
            {
                var numberOfTreasuresRecovered = building.Items.Where(x => x.IsTreasure).Count();
                thePlayer.Score += 15 * numberOfTreasuresRecovered;
            }

            if (player.EventRecord.ContainsKey(EventIds.Dwarves))
            {
                thePlayer.Score += 5 * player.EventRecord[EventIds.Dwarves];
            }

            player.ChatClient.PostDirectMessage(player, 
                $"You earned {thePlayer.Score} during your Adventure in {player.Moves} moves.");

            _players.Remove(thePlayer);
        }
    }
}