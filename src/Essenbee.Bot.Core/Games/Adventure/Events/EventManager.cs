using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Events
{
    public static class EventManager
    {
        public static void CheckForEvents(IAdventurePlayer player)
        {
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
