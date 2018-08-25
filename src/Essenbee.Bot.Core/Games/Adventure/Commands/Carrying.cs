namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Carrying : IAdventureCommand
    {
        private readonly IReadonlyAdventureGame _game;

        public Carrying(IReadonlyAdventureGame game)
        {
            _game = game;
        }

        public void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            player.ChatClient.PostDirectMessage(player.Id, player.Inventory.ListItems());
        }
    }
}
