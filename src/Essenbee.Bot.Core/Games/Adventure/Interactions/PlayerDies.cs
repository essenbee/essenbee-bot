using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class PlayerDies : IAction
    {
        private readonly IReadonlyAdventureGame _game;

        public PlayerDies(IReadonlyAdventureGame game)
        {
            _game = game;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item)
        {
            player.Statuses.Add(PlayerStatus.IsDead);
            _game.EndOfGame(player);
            return true;
        }
    }
}
