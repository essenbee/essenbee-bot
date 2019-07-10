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
                            // Not in same room as the player, so build a list of possible places to move to...
                            var possibleMoves = new List<Location>();

                            var movesAvailable = dwarf.MonsterValidMoves.Count > 0
                                ? dwarf.MonsterValidMoves
                                : dwarf.ValidMoves;

                            // ToDo: set correct movement rules, like these:
                            // - if player is "nearby", move towards them; but
                            //   should player be in a dead end, wait at the exit
                            // - otherwise move to a valid location that is:
                            //   * not a dead end
                            //   * on Level = 1

                            foreach (var move in movesAvailable)
                            {
                                var found = game.Dungeon.TryGetLocation(move.Destination, out var potentialMove);

                                if (found && potentialMove.Level == 1 && 
                                    potentialMove.NumberOfExits > 1 && 
                                    move.Destination != dwarf.LocationId)
                                {
                                    possibleMoves.Add(move.Destination);
                                }
                            }

                            var moveToLocation = 0;

                            if (possibleMoves.Count > 1)
                            {
                                var dieRoll = GetRandomNumber(0, 99);
                                moveToLocation = (int)(dieRoll * possibleMoves.Count / 100);
                            }

                            game.Dungeon.TryGetLocation(possibleMoves[moveToLocation], out var newLocation);

                            game.WanderingMonsters[i] = newLocation;
                        }
                    }

                    i++;
                }

                var numDwarfs = 0;

                foreach (var dwarf in game.WanderingMonsters)
                {
                    if (player.CurrentLocation == dwarf)
                    {
                        numDwarfs++;
                    }
                }

                if (numDwarfs > 0)
                {
                    if (numDwarfs == 1)
                    {
                        player.ChatClient.PostDirectMessage(player, "There is a nasty-looking dwarf in the room with you!");
                        player.ChatClient.PostDirectMessage(player, "The dwarf lunges at you with a wickedly sharp knife!");
                    }
                    else
                    {
                        player.ChatClient.PostDirectMessage(player, $"There are {numDwarfs} nasty-looking dwarfs in the room with you!");
                        player.ChatClient.PostDirectMessage(player, "The dwarfs lunge at you with wickedly sharp knives!");
                    }

                    var numberOfHits = RollToHit(numDwarfs);

                    if (numberOfHits > 0)
                    {
                        // Player is hit by the knife and dies?
                        var woundDescr = numberOfHits == 1 
                            ? "A knife sinks into your flesh" 
                            : $"{numberOfHits} knives pierce your body";
                        player.ChatClient.PostDirectMessage(player, $"{woundDescr} and you feel your " +
                            "lifeblood ebbing away...");
                        player.Statuses.Add(PlayerStatus.IsDead);
                        game.EndOfGame(player);
                    }
                    else
                    {
                        player.ChatClient.PostDirectMessage(player, "Phew - missed! You deftly dodge out of the way of danger!");
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

        private static int RollToHit(int numDwarfs)
        {
            var hits = 0;

            for (int i = 0; i < numDwarfs; i++)
            {
                var toHitRoll = GetRandomNumber(1, 100);
                if (toHitRoll < 26)
                {
                    hits++;
                }
            }

            return hits;
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
