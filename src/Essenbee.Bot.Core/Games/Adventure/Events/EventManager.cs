using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System;
using System.Collections.Generic;

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

            if (player.EventRecord.ContainsKey(EventIds.Dwarves))
            {
                var dwarfs = game.WanderingMonsters.ToArray();

                var i = 0;

                foreach (var dwarf in dwarfs)
                {
                    if (dwarf.LocationId != Location.Nowhere)
                    {
                        // Dwarf is not dead
                        if (dwarf != player.CurrentLocation)
                        {
                            // Not in same room as the player, so move
                            var possibleMoves = new List<Location>();
                            foreach (var move in dwarf.ValidMoves)
                            {
                                var found = game.Dungeon.TryGetLocation(move.Destination, out var potentialMove);

                                if (found && potentialMove.Level == 1 && potentialMove.NumberOfExits > 1)
                                {
                                    possibleMoves.Add(move.Destination);
                                }
                            }

                            var moveToLocation = GetRandomNumber(1, possibleMoves.Count) - 1;

                            game.Dungeon.TryGetLocation(possibleMoves[moveToLocation], out var newLocation);

                            game.WanderingMonsters[i] = newLocation;
                        }
                        else
                        {
                            // In room with player!
                            player.ChatClient.PostDirectMessage(player, "There is an angry-looking dwarf in the " +
                                "room with you!");
                            player.ChatClient.PostDirectMessage(player, "The dwarf lunges at you with a wickedly sharp knife!");

                            var toHitRoll = GetRandomNumber(1, 100);
                            if (toHitRoll < 26)
                            {
                                // Player is hit by the knife and dies?
                                player.ChatClient.PostDirectMessage(player, "The knife sinks into your flesh and you feel your " +
                                    "lifeblood ebbing away...");
                                player.Statuses.Add(PlayerStatus.IsDead);
                                game.EndOfGame(player);
                                break;
                            }
                        }
                    }

                    i++;
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
