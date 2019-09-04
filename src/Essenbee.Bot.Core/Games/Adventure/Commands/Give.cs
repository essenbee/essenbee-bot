using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Give : BaseAdventureCommand
    {
        public Give(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
            CheckEvents = false;
            IsVerbatim = false;
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var args = e.ArgsAsList;

            if (args.Count == 1)
            {
                player.ChatClient.PostDirectMessage(player, "What would you like to give?");
                return;
            }

            IAdventureItem itemInInventory = null;
            var giveTo = string.Empty;

            if (args.Count == 2)
            {
                var itemToGive = args[1].ToLower();

                if (!player.Inventory.Has(itemToGive))
                {
                    player.ChatClient.PostDirectMessage(player, $"You don't have a {itemToGive} to give to anyone!");
                    return;
                }

                itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(itemToGive));
            }
            else
            {
                var potentialItem = args[1].ToLower();
                giveTo = args[2].ToLower();

                if (!player.Inventory.Has(potentialItem))
                {
                    giveTo = args[1].ToLower();
                    potentialItem = args[2].ToLower();

                    if (!player.Inventory.Has(potentialItem))
                    {
                        player.ChatClient.PostDirectMessage(player, $"You don't have a {potentialItem} to give to anyone!");
                        player.ChatClient.PostDirectMessage(player, "Try saying, !adv give _item_ to _recipient_...");

                        return;
                    }
                    else
                    {
                        itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(potentialItem));
                    }
                }
                else
                {
                    itemInInventory = player.Inventory.GetItems().FirstOrDefault(i => i.IsMatch(potentialItem));
                }
            }


            if (player.Here(giveTo))
            {
                var recipient = player.CurrentLocation.Items.FirstOrDefault(i => i.IsMatch(giveTo));

                if (player.Here(Item.Troll))
                {
                    // Destroy item
                    var removeItem = new RemoveFromInventory();
                    removeItem.Do(player, itemInInventory);

                    if (itemInInventory.IsTreasure)
                    {
                        // Troll will move away from the bridge
                        player.ChatClient.PostDirectMessage(player,
                            $"The troll accepts your {itemInInventory.Name}, appraises it with a critical eye, and retreats under its bridge!");
                    }
                    else
                    {
                        // The troll throws the item into the chasm
                        player.ChatClient.PostDirectMessage(player,
                            $"The troll scowls at you and throws the {itemInInventory.Name} into the chasm. 'Pay Troll!', it roars.");

                        return;
                    }

                    _game.Dungeon.TryGetLocation(Location.SouthWestOfChasm, out var swOfChasm);
                    _game.Dungeon.TryGetLocation(Location.NorthEastOfChasm, out var neOfChasm);
                    var removeTroll1 = new RemoveFromLocation(recipient, swOfChasm);
                    var removeTroll2 = new RemoveFromLocation(recipient, neOfChasm);
                    removeTroll1.Do(player, recipient);
                    removeTroll2.Do(player, recipient);

                    // Open up passage over the bridge
                    var addMove1 = new AddMoves(new List<IPlayerMove>
                        {
                            new PlayerMove(string.Empty, Location.TrollBridge, "northeast", "ne"),
                        }, _game, Location.SouthWestOfChasm);
                    addMove1.Do(player, null);

                    var addMove2 = new AddMoves(new List<IPlayerMove>
                        {
                            new PlayerMove(string.Empty, Location.TrollBridge, "southwest", "sw"),
                        }, _game, Location.NorthEastOfChasm);
                    addMove2.Do(player, null);

                    // After player has crossed, Troll returns to guard bridge

                    return;
                }

            }

            player.ChatClient.PostDirectMessage(player, $"I'm not sure who you want to give the {itemInInventory.Name} to...");

        }
    }
}
