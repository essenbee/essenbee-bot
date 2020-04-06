using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Attack : BaseAdventureCommand
    {
        public Attack(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
            CheckEvents = false;
            IsVerbatim = false;
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var args = e.ArgsAsList;

            if (args.Count == 1)
            {
                player.ChatClient.PostDirectMessage(player, "What would you like to attack?");
                return;
            }
            else
            {
                var victim = args[1].ToLower();
                
                if (player.Here(victim))
                {
                    var target = player.CurrentLocation.Items.FirstOrDefault(i => i.IsMatch(victim));

                    if (target == null)
                    {
                        var contents = player.CurrentLocation.Items.SelectMany(i => i.Contents);
                        target = contents.FirstOrDefault(i => i.IsMatch(victim));
                    }

                    if (target != null)
                    {
                        target.Attack(player, target);
                    }
                }
            }
        }
    }
}
