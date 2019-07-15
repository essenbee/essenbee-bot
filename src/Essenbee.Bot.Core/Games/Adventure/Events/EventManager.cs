using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Events
{
    public static class EventManager
    {
        private static readonly Random getRandom = new Random();

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

            //Dwarves
            if (player.CurrentLocation.Level == 1)
            {
                if (!player.EventRecord.ContainsKey(EventIds.Dwarves))
                {
                    if (GetRandomNumber(1, 6) <= 2)
                    {
                        player.EventRecord.Add(EventIds.Dwarves, 1);
                        player.ChatClient.PostDirectMessage(player, "A hunched dwarf just walked " +
                            "around a corner, saw you, threw a little axe at you (which missed), cursed, and ran away.");
                        var addItem = new AddToLocation(ItemFactory.GetInstance(game, Item.LittleAxe),
                            player.CurrentLocation);
                        addItem.Do(null, null);

                        return;
                    }
                }
            }

            if (player.EventRecord.ContainsKey(EventIds.Dwarves))
            {
                if (game.WanderingMonsters.Count == 0)
                {
                    SpawnWanderingMonsters(game, player.CurrentLocation);
                }

                var dwarfs = game.WanderingMonsters.Where(d => d.CurrentLocation != null).ToArray();

                foreach (var dwarf in dwarfs)
                {
                    if (dwarf.CurrentLocation.LocationId != Location.Nowhere)
                    {
                        // Dwarf is not dead
                        var possibleMoves = new List<Location>();

                        var movesAvailable = (dwarf.CurrentLocation.MonsterValidMoves.Count > 0)
                            ? dwarf.CurrentLocation.MonsterValidMoves
                            : dwarf.CurrentLocation.ValidMoves;

                        foreach (var move in movesAvailable)
                        {
                            var found = game.Dungeon.TryGetLocation(move.Destination, out var potentialMove);

                            if (found && (potentialMove.Level == 1) &&
                                (potentialMove.NumberOfExits > 1) &&
                                (move.Destination != dwarf.CurrentLocation.LocationId))
                            {
                                possibleMoves.Add(move.Destination);
                            }
                        }

                        // Has the dwarf seen the player?
                        if ((dwarf.HasSeenPlayer && (player.CurrentLocation.Level == 1)) ||
                            dwarf.PrevLocation.LocationId.Equals(player.CurrentLocation.LocationId) ||
                            dwarf.CurrentLocation.LocationId.Equals(player.CurrentLocation.LocationId))
                        {
                            // Stick with the player
                            dwarf.HasSeenPlayer = true;
                            dwarf.PrevLocation = dwarf.CurrentLocation;
                            dwarf.CurrentLocation = player.CurrentLocation;

                            continue;
                        }

                        dwarf.HasSeenPlayer = false;

                        // Random movement
                        var moveToLocation = 0;

                        if (possibleMoves.Count > 1)
                        {
                            var dieRoll = GetRandomNumber(0, 99);
                            moveToLocation = (dieRoll * possibleMoves.Count) / 100;
                        }

                        game.Dungeon.TryGetLocation(possibleMoves[moveToLocation], out var newLocation);

                        dwarf.PrevLocation = dwarf.CurrentLocation;
                        dwarf.CurrentLocation = newLocation;
                    }
                }

                var numDwarfs = 0;
                var numAttacks = 0;

                foreach (var dwarf in game.WanderingMonsters.Where(d => d.CurrentLocation != null))
                {
                    if (player.CurrentLocation.LocationId.Equals(dwarf.CurrentLocation.LocationId))
                    {
                        // A dwarf is in the room with the player
                        numDwarfs++;
                        if (dwarf.CurrentLocation.LocationId.Equals(dwarf.PrevLocation.LocationId))
                        {
                            numAttacks++;
                        }
                    }
                }

                if (numDwarfs > 0)
                {
                    if (numDwarfs == 1)
                    {
                        player.ChatClient.PostDirectMessage(player, "There is a nasty-looking dwarf in the room with you!");

                        if (numAttacks == 1)
                        {
                            player.ChatClient.PostDirectMessage(player, "The dwarf lunges at you with a wickedly sharp knife!");
                        }
                    }
                    else
                    {
                        player.ChatClient.PostDirectMessage(player, $"There are {numDwarfs} nasty-looking dwarfs in the room with you!");

                        if (numAttacks > 0)
                        {
                            player.ChatClient.PostDirectMessage(player, "The dwarfs lunge at you with wickedly sharp knives!");
                        }
                    }

                    if (numAttacks > 0)
                    {
                        var numberOfHits = RollToHit(numAttacks);

                        if (numberOfHits > 0)
                        {
                            // Player is hit by the knife and dies?
                            var woundDescr = (numberOfHits == 1)
                                ? "A knife sinks into your flesh"
                                : ($"{numberOfHits} knives pierce your body");
                            player.ChatClient.PostDirectMessage(player, ($"{woundDescr} and you feel your ") +
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

        private static void SpawnWanderingMonsters(IReadonlyAdventureGame game, IAdventureLocation playerLocation)
        {
            // Initial wandering monster locations
            game.Dungeon.TryGetLocation(Location.HallOfMountainKing, out var location1);
            game.WanderingMonsters.Add(new WanderingMonster(location1));
            game.Dungeon.TryGetLocation(Location.FissureWest, out var location2);
            game.WanderingMonsters.Add(new WanderingMonster(location2));
            game.Dungeon.TryGetLocation(Location.Y2, out var location3);
            game.WanderingMonsters.Add(new WanderingMonster(location3));
            game.Dungeon.TryGetLocation(Location.AllAlike3, out var location4);
            game.WanderingMonsters.Add(new WanderingMonster(location4));
            //game.Dungeon.TryGetLocation(Locations.Location.ComplexJunction, out var location5);
            //game.WanderingMonsters.Add(new WanderingMonster(location5));
            game.Dungeon.TryGetLocation(Location.PirateChestCave, out var location6);
            game.WanderingMonsters.Add(new WanderingMonster(location6));

            game.Dungeon.TryGetLocation(Location.GoldRoom, out var altLocation);

            // Do not spawn a dwarf on top of the player!
            foreach (var monster in game.WanderingMonsters)
            {
                if (monster.CurrentLocation.LocationId.Equals(playerLocation.LocationId))
                {
                    monster.CurrentLocation = altLocation;
                    monster.PrevLocation = altLocation;
                    break;
                }
            }
        }

        private static int RollToHit(int numAttacks)
        {
            var hits = 0;

            for (var i = 0; i < numAttacks; i++)
            {
                var toHitRoll = GetRandomNumber(0, 99);
                if (toHitRoll < 20)
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
