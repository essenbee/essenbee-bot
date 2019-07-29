using System.Collections.Generic;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Linq;
using Essenbee.Bot.Core.Games.Adventure.Events;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class DwarfManager : IMonsterManager
    {
        public List<WanderingMonster> Monsters { get; } = new List<WanderingMonster>();

        public Dictionary<Location, IAdventureLocation> Locations { get; }

        public DwarfManager(Dictionary<Location, IAdventureLocation> locations)
        {
            Locations = locations;
        }

        public void Act(IReadonlyAdventureGame game, IAdventurePlayer player)
        {
            if (player.CurrentLocation.Level == 1)
            {
                if (!player.EventRecord.ContainsKey(EventIds.Dwarves))
                {
                    if (DieRoller.SixSider(2))
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
                if (Monsters.Count == 0)
                {
                    SpawnDwarfs(player.CurrentLocation);
                    return;
                }

                var dwarfs = Monsters.Where(d => d.CurrentLocation != null).ToArray();

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
                            var dieRoll = DieRoller.Percentage();
                            moveToLocation = (dieRoll * possibleMoves.Count) / 100;
                        }

                        game.Dungeon.TryGetLocation(possibleMoves[moveToLocation], out var newLocation);

                        dwarf.PrevLocation = dwarf.CurrentLocation;
                        dwarf.CurrentLocation = newLocation;
                    }
                }

                var numDwarfs = 0;
                var numAttacks = 0;

                foreach (var dwarf in Monsters.Where(d => d.CurrentLocation != null))
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
        }

        private void SpawnDwarfs(IAdventureLocation playerLocation)
        {
            var spawnPoints = Locations.Values.Where(l => l.IsSpawnPoint && l.SpawnType == MonsterGroup.Dwarves);
            var altSpawnPoint = Locations.Values.Where(l => l.IsAlternateSpawnPoint && 
                l.SpawnType == MonsterGroup.Dwarves).FirstOrDefault();

            foreach (var spawnPoint in spawnPoints)
            {
                Monsters.Add(new WanderingMonster(spawnPoint, MonsterGroup.Dwarves));
            }

            if (altSpawnPoint != null)
            {
                // Do not spawn a dwarf on top of the player!
                foreach (var monster in Monsters)
                {
                    if (monster.CurrentLocation.LocationId.Equals(playerLocation.LocationId))
                    {
                        monster.CurrentLocation = altSpawnPoint;
                        monster.PrevLocation = altSpawnPoint;
                        break;
                    }
                }
            }
        }

        private static int RollToHit(int numAttacks)
        {
            var hits = 0;

            for (var i = 0; i < numAttacks; i++)
            {
                if (DieRoller.Percentage(20))
                {
                    hits++;
                }
            }

            return hits;
        }
    }
}
