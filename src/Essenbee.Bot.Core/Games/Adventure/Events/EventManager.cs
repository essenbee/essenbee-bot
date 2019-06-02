using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System;

namespace Essenbee.Bot.Core.Games.Adventure.Events
{
    public static class EventManager
    {
        private static readonly Random getRandom = new Random();

        public static void CheckForEvents(IAdventurePlayer player)
        {
            if (player.CurrentLocation.LocationId.Equals(Location.HallOfMistsEast))
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

            //Dwarves
            if (player.CurrentLocation.Level == 1)
            {
                if (!player.EventRecord.ContainsKey(EventIds.Dwarves))
                {
                    if (GetRandomNumber(1, 6) <= 2)
                    {
                        player.EventRecord.Add(EventIds.Dwarves, 0);
                        player.ChatClient.PostDirectMessage(player, "A hunched dwarf just walked " +
                            "around a corner, saw you, threw a little axe at you (which missed), cursed, and ran away.");
                        var addItem = new AddToLocation(ItemFactory.GetInstance(game, Item.LittleAxe),
                            player.CurrentLocation);
                        addItem.Do(null, null);
                    }
                }
            }

            // Handle dead dragon -> rotting dead dragon
            if (player.Clocks.ContainsKey("dragon") && player.Clocks["dragon"] > 10)
            {
                game.Dungeon.TryGetLocation(Location.SecretNorthEastCanyon, out var location);
                
                var remove = new RemoveFromLocation(ItemFactory.GetInstance(game, Item.DeadDragon), location);
                remove.Do(null, null);
                var addItem = new AddToLocation(ItemFactory.GetInstance(game, Item.RottingDeadDragon), location);
                addItem.Do(null, null);

                player.Clocks.Remove("dragon");
            }
        }

        private static int GetRandomNumber(int min, int max)
        {
            lock (getRandom) // synchronize
            {
                return getRandom.Next(min, max);
            }
        }
    }
}
