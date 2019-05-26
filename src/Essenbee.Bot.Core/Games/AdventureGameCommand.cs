using System;
using System.Threading.Tasks;
using Essenbee.Bot.Core.Games.Adventure;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot.Core.Games
{
    public class AdventureGameCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName => "adv";
        public string HelpText => "Play an old school text adventure game";
        public TimeSpan Cooldown { get; }

        private AdventureGame _adventureGame;
        private object _mutex = new object();
        private readonly IBot _bot;

        public AdventureGameCommand(IBot bot)
        {
            _bot = bot;
            Cooldown = TimeSpan.FromMinutes(0);
        }

        public Task Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            lock (_mutex)
            {
                if (chatClient.GetType().ToString() == e.ClientType)
                {
                    if (_adventureGame is null)
                    {
                        _adventureGame = new AdventureGame();
                    }

                    _adventureGame.HandleCommand(chatClient, e);
                }
            }

            return null;
        }

        public bool ShouldExecute() => true;
    }
}
