using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    // ToDo: Implement ability to attack wandering monsters i.e. Dwarves!
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
            else if (args.Count == 2)
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
                        if (target.IsCreature)
                        {
                            player.ChatClient.PostDirectMessage(player, "Do you just want to use your bare hands?");
                            var attackState = new AddPlayerItemState("attack"); // ToDo: how to guarantee this is removed?
                            attackState.Do(player, target);
                        }
                        else
                        {
                            target.Attack(player, target);
                        }
                    }

                    return;
                }
            }
            else
            {
                var victim = args[1].ToLower();
                var weapon = args[2].ToLower();

                var target = player.CurrentLocation.Items.FirstOrDefault(i => i.IsMatch(victim));

                if (target == null)
                {
                    var contents = player.CurrentLocation.Items.SelectMany(i => i.Contents);
                    target = contents.FirstOrDefault(i => i.IsMatch(victim));
                }

                if (target != null)
                {
                    if (!player.Inventory.Has(weapon))
                    {
                        player.ChatClient.PostDirectMessage(player, $"You are not carrying a {weapon}!");
                        return;
                    }

                    var weaponItem = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(weapon));

                    if (!weaponItem.IsWeapon)
                    {
                        player.ChatClient.PostDirectMessage(player, $"You cannot fight with a {weapon}!");
                        return;
                    }

                    target.Attack(player, target, weaponItem);
                    return;
                }
            }

            player.ChatClient.PostDirectMessage(player, $"I can't see anything like that to attack!");
        }
    }
}
