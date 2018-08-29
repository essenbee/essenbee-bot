using Essenbee.Bot.Core.Games.Adventure;
using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot.Core.Games
{
    public class AdventureGameCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName => "adv";
        public string HelpText => "Play an old school text adventure game";

        private AdventureGame _adventureGame;
        private object _mutex = new object();
        private readonly IBot _bot;

        public AdventureGameCommand(IBot bot)
        {
            _bot = bot;
        }

        public void Execute(IChatClient chatClient, ChatCommandEventArgs e)
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
        }

        public bool ShouldExecute() => true;
    }
}
