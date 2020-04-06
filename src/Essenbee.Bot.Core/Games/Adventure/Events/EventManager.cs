using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Events
{
    public static class EventManager
    {
        private const string TheDragon = "dragon";
        private const string TheTroll = "troll";
        public static void CheckForEvents(IAdventurePlayer player)
        {
            if (player.Statuses.Contains(PlayerStatus.IsDead))
            {
                return;
            }

            // Item Event processing...
            foreach (var carriedItem in player.Inventory.GetItems())
            {
                carriedItem.RunItemEvents(player, carriedItem);    
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
            if (player.Clocks.ContainsKey(TheDragon) && (player.Clocks[TheDragon] > 10))
            {
                game.Dungeon.TryGetLocation(Location.SecretNorthEastCanyon, out var location);

                var remove = new RemoveFromLocation(ItemFactory.GetInstance(game, Item.DeadDragon), location);
                remove.Do(null, null);
                var addItem = new AddToLocation(ItemFactory.GetInstance(game, Item.RottingDeadDragon), location);
                addItem.Do(null, null);

                player.Clocks.Remove(TheDragon);
            }

            // Handle return of troll to guard its bridge
            if (player.Clocks.ContainsKey(TheTroll) && (player.Clocks[TheTroll] > 5))
            {
                var troll = ItemFactory.GetInstance(game, Item.Troll);

                game.Dungeon.TryGetLocation(Location.SouthWestOfChasm, out var swOfChasm);
                game.Dungeon.TryGetLocation(Location.NorthEastOfChasm, out var neOfChasm);
                var addTroll1 = new AddToLocation(troll, swOfChasm);
                var addTroll2 = new AddToLocation(troll, neOfChasm);
                addTroll1.Do(player, troll);
                addTroll2.Do(player, troll);

                // Block access to Troll Bridge
                var denyPassage1 = new RemoveDestination(game, Location.TrollBridge, swOfChasm.LocationId);
                var denyPassage2 = new RemoveDestination(game, Location.TrollBridge, neOfChasm.LocationId);
                denyPassage1.Do(player, troll);
                denyPassage2.Do(player, troll);

                player.Clocks.Remove(TheTroll);
            }
        }
    }
}
