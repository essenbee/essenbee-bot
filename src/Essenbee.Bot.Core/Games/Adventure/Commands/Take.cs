using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Take : BaseAdventureCommand
    {
        private const string All = "all";

        public Take(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs) => CheckEvents = true;

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var location = player.CurrentLocation;
            var item = e.ArgsAsList[1].ToLower();
            var containers = location.Items.Where(i => i.IsContainer && (i.Contents.Count > 0)).ToList();

            if (item.Equals(All))
            {
                var portableItems = location.Items.Where(x => x.IsPortable).ToList();

                foreach (var anItem in portableItems)
                {
                    if ((anItem.ContainerRequired() && !player.Inventory.HasRequiredContainer(anItem)) ||
                        player.Inventory.Has(anItem.PreventTakeItemId))
                    {
                        continue;
                    }

                    if (player.Inventory.Has(anItem.PreventTakeItemId))
                    {
                        continue;
                    }

                    PickUpItem(player, anItem);
                }

                PickUpContainedItems(player, containers, All);
            }
            else
            {
                var locationItem = location.Items.FirstOrDefault(i => i.IsMatch(item));

                if (locationItem != null)
                {
                    // Cannot pick up another lamp from the Small Building
                    if ((locationItem.ItemId == Item.Lamp) &&
                        player.CurrentLocation.LocationId.Equals(Location.Building) &&
                        player.EventRecord.ContainsKey(Events.EventIds.HasLamp))
                    {
                        locationItem = null;
                    }
                }

                if ((locationItem != null) && player.Inventory.GetItems().Any(i => i.ItemId.Equals(locationItem.ItemId)))
                {
                    player.ChatClient.PostDirectMessage(player, $"You are already carrying a {item} with you.");
                    return;
                }

                if (PickUpContainedItems(player, containers, item))
                {
                    return;
                }

                if (locationItem is null)
                {
                    player.ChatClient.PostDirectMessage(player, $"You cannot see a {item} here!");
                    return;
                }

                if (!locationItem.IsPortable)
                {
                    player.ChatClient.PostDirectMessage(player, $"You cannot carry the {item} with you!");
                    return;
                }

                if (locationItem.ContainerRequired() && !player.Inventory.HasRequiredContainer(locationItem))
                {
                    player.ChatClient.PostDirectMessage(player, $"You have no way of carrying a {item} with you...");
                    return;
                }

                if (player.Inventory.Has(locationItem.PreventTakeItemId))
                {
                    player.ChatClient.PostDirectMessage(player, locationItem.PreventTakeText);
                    return;
                }

                if (locationItem.IsEndlessSupply)
                {
                    CreateNewInstance(player, locationItem);
                    return;
                }

                PickUpItem(player, locationItem);
            }
        }

        private static bool PickUpContainedItems(IAdventurePlayer player, List<IAdventureItem> containers, string item)
        {
            var retVal = false;

            foreach (var container in containers)
            {
                var theContents = container.Contents.ToList();
                foreach (var containedItem in theContents)
                {
                    if (!item.Equals("all") && containedItem.IsMatch(item) && containedItem.IsPortable)
                    {
                        GetContaineredItem(container, containedItem);
                        return true;
                    }
                    else if (containedItem.IsPortable)
                    {
                        GetContaineredItem(container, containedItem);
                        retVal = retVal || true;
                    }
                }
            }

            return retVal;

            void GetContaineredItem(IAdventureItem container, IAdventureItem containedItem)
            {
                player.Inventory.AddItem(containedItem);
                container.Contents.Remove(containedItem);
                player.ChatClient.PostDirectMessage(player, $"You are now carrying a {containedItem.Name} with you.");
            }
        }

        private static void PickUpItem(IAdventurePlayer player, IAdventureItem anItem)
        {
            var okay = anItem.ContainerRequired()
                ? player.Inventory.AddItemToContainer(anItem, anItem.MustBeContainedIn)
                : player.Inventory.AddItem(anItem);

            player.CurrentLocation.Items.Remove(anItem);
            anItem.AddPlayerStatusCondition(player, anItem.GivesPlayerStatus);

            player.ChatClient.PostDirectMessage(player, $"You are now carrying a {anItem.Name} with you.");
        }

        private void CreateNewInstance(IAdventurePlayer player, IAdventureItem locationItem)
        {
            if (locationItem.ContainerRequired())
            {
                player.Inventory.AddItemToContainer(ItemFactory.GetInstance(_game, locationItem.ItemId),
                    locationItem.MustBeContainedIn);
            }
            else
            {
                player.Inventory.AddItem(ItemFactory.GetInstance(_game, locationItem.ItemId));
            }
        }
    }
}
