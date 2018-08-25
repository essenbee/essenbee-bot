namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Carrying : BaseAdventureCommand
    {
        public Carrying(IReadonlyAdventureGame game, params string[] verbs) : base (game, verbs)
        {
        }

        public override void Invoke(AdventurePlayer player, ChatCommandEventArgs e)
        {
            player.ChatClient.PostDirectMessage(player.Id, player.Inventory.ListItems());
        }
    }
}
