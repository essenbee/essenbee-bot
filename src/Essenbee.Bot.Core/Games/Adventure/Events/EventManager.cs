using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Events
{
    public static class EventManager
    {
        public static void CheckForEvents(IAdventurePlayer player)
        {
            if (player.Statuses.Contains(PlayerStatus.IsDead))
            {
                return;
            }

            // Lamp processing
            var lamp = player.Inventory.GetItems().FirstOrDefault(x => x.ItemId == Item.Lamp);
            if (lamp != null)
            {
                if (!player.EventRecord.ContainsKey(EventIds.HasLamp))
                {
                    player.EventRecord.Add(EventIds.HasLamp, 330);
                }
                else
                {
                    if (lamp.IsActive)
                    {
                        player.EventRecord[EventIds.HasLamp]--;
                    }
                }
            }

            if (player.EventRecord.ContainsKey(EventIds.HasLamp))
            {
                var lampTurns = player.EventRecord[EventIds.HasLamp];
                var theLamp = player.Inventory.GetItems().FirstOrDefault(x => x.ItemId == Item.Lamp);

                if (theLamp != null)
                {
                    if (lampTurns > 0 && lampTurns <= 30)
                    {
                        player.ChatClient.PostDirectMessage(player, "Your lamp is going dim!");
                    }

                    if (lampTurns <= 0)
                    {
                        theLamp.IsActive = false;
                    }
                }
            }

            if (player.CurrentLocation.Level.Equals(1))
            {
                if (!player.EventRecord.ContainsKey(EventIds.CaveOpen))
                {
                    // Cave is opened
                    player.EventRecord.Add(EventIds.CaveOpen, 1);
                    player.Score += 25;
                }
            }

            if (!player.EventRecord.ContainsKey(EventIds.CaveOpen))
            {
                return;
            }

            var game = player.CurrentLocation.Game;

            // Monsters act
            foreach (var manager in game.MonsterManagers)
            {
                manager.Act(game, player);
            }

            // Points for recovering stolen treasure
            if (!player.EventRecord.ContainsKey(EventIds.RecoveredStolenTreasure))
            {
                if (player.CurrentLocation.LocationId == Location.PirateChestCave)
                {
                    var found = game.Dungeon.TryGetLocation(Location.PirateChestCave, out var pirateChest);
                    if (found && pirateChest.Items.Where(x => x.IsTreasure).Count() > 0)
                    {
                        player.EventRecord.Add(EventIds.RecoveredStolenTreasure, 1);
                        player.Score += 10;
                    }
                }
            }

            // Handle dead dragon -> rotting dead dragon
            if (player.Clocks.ContainsKey("dragon") && (player.Clocks["dragon"] > 10))
            {
                game.Dungeon.TryGetLocation(Location.SecretNorthEastCanyon, out var location);

                var remove = new RemoveFromLocation(ItemFactory.GetInstance(game, Item.DeadDragon), location);
                remove.Do(null, null);
                var addItem = new AddToLocation(ItemFactory.GetInstance(game, Item.RottingDeadDragon), location);
                addItem.Do(null, null);

                player.Clocks.Remove("dragon");
            }
        }
    }
}
