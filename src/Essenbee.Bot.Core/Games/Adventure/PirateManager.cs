using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class PirateManager : IMonsterManager
    {
        public List<WanderingMonster> Monsters { get; } = new List<WanderingMonster>();
        public Dictionary<Location, IAdventureLocation> Locations { get; }

        public PirateManager(Dictionary<Location, IAdventureLocation> locations)
        {
            Locations = locations;
        }

        public void Act(IReadonlyAdventureGame game, IAdventurePlayer player)
        {
            if (Monsters.Count == 0)
            {
                SpawnPirates();
                return;
            }

            var pirates = Monsters.Where(d => d.CurrentLocation != null).ToArray();

            foreach (var pirate in pirates)
            {
                if (pirate.CurrentLocation.LocationId != Location.Nowhere)
                {
                    // Pirate is still active
                    var possibleMoves = new List<Location>();

                    var movesAvailable = (pirate.CurrentLocation.MonsterValidMoves.Count > 0)
                        ? pirate.CurrentLocation.MonsterValidMoves
                        : pirate.CurrentLocation.ValidMoves;

                    foreach (var move in movesAvailable)
                    {
                        var found = game.Dungeon.TryGetLocation(move.Destination, out var potentialMove);

                        if (found && (potentialMove.Level == 1) &&
                            (potentialMove.NumberOfExits > 1) &&
                            (move.Destination != pirate.CurrentLocation.LocationId))
                        {
                            possibleMoves.Add(move.Destination);
                        }
                    }

                    // Has the pirate seen the player?
                    if ((pirate.HasSeenPlayer && (player.CurrentLocation.Level == 1)) ||
                        pirate.CurrentLocation.LocationId.Equals(player.CurrentLocation.LocationId))
                    {
                        // Stick with the player
                        pirate.HasSeenPlayer = true;
                        pirate.PrevLocation = pirate.CurrentLocation;
                        pirate.CurrentLocation = player.CurrentLocation;

                        player.ChatClient.PostDirectMessage(player, "You hear a rustling noise behind you...");
                    }
                    else
                    {
                        pirate.HasSeenPlayer = false;
                    }

                    var playerLocation = player.CurrentLocation.LocationId;

                    if (possibleMoves.Contains(playerLocation) && !pirate.HasSeenPlayer)
                    {
                        // Pirate is 1 move away from the player
                        player.ChatClient.PostDirectMessage(player, "You hear a rustling noise in the darkness...");
                        continue;
                    }

                    if (!pirate.HasSeenPlayer)
                    {
                        // Random movement
                        var moveToLocation = 0;

                        if (possibleMoves.Count > 1)
                        {
                            var dieRoll = DieRoller.Percentage();
                            moveToLocation = (dieRoll * possibleMoves.Count) / 100;
                        }

                        game.Dungeon.TryGetLocation(possibleMoves[moveToLocation], out var newLocation);

                        pirate.PrevLocation = pirate.CurrentLocation;
                        pirate.CurrentLocation = newLocation;

                        continue;
                    }

                    // Steal treasure?
                    if (player.Inventory.HasTreasure())
                    {
                        if (DieRoller.Percentage(49))
                        {
                            var found = game.Dungeon.TryGetLocation(Location.PirateChestCave, out var pirateChest);

                            if (found)
                            {
                                var treasures = player.Inventory.GetTreasures();

                                foreach (var treasure in treasures)
                                {
                                    player.Inventory.RemoveItem(treasure);
                                    var addItem = new AddToLocation(treasure, pirateChest);
                                    addItem.Do(null, null);
                                }

                                game.Dungeon.TryGetLocation(Location.Nowhere, out var nowhere);
                                pirate.CurrentLocation = nowhere;
                                pirate.PrevLocation = nowhere;

                                player.ChatClient.PostDirectMessage(player, "...and a pirate jumps out of the darkness!");
                                player.ChatClient.PostDirectMessage(player, "Before you can react, he has swiped all of " +
                                    "your treasure and vanished into the gloom! As he leaves, you hear the pirate laughing: " +
                                    "'I'll hide this treasure in my maze!'");
                            }
                        }
                    }
                    else
                    {
                        pirate.HasSeenPlayer = false;
                    }
                }
            }
        }

        private void SpawnPirates()
        {
            var spawnPoints = Locations.Values.Where(l => l.IsSpawnPoint && l.SpawnType == MonsterGroup.Pirate);

            foreach (var spawnPoint in spawnPoints)
            {
                Monsters.Add(new WanderingMonster(spawnPoint, MonsterGroup.Pirate));
            }
        }
    }
}
