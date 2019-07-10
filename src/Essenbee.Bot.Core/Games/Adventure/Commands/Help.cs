using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Help : BaseAdventureCommand
    {
        public Help(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
            CheckEvents = false;
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var helpText = new StringBuilder("I know several commands to aid you in your exploration, including:");
            helpText.AppendLine();
            helpText.AppendLine("\t`!adv look`");
            helpText.AppendLine("\t`!adv go`");
            helpText.AppendLine("\t`!adv take`");
            helpText.AppendLine("\t`!adv use`");
            helpText.AppendLine("\t`!adv inventory`");
            helpText.AppendLine();
            helpText.AppendLine("Some of the places you will visit have items lying around. If such an item is shown in *bold* text, you can take it and carry it with you; it may or may not be of any use.");
            helpText.AppendLine("Note that, if you find several of the same item, you can only carry one of them at a time!");
            player.ChatClient.PostDirectMessage(player, helpText.ToString());
        }
    }
}
