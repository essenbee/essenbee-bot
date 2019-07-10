using Essenbee.Bot.Core.Games.Adventure.Events;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Teleport : BaseAdventureCommand
    {

        public Teleport(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
            CheckEvents = true;
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var moveTo = Location.HallOfMountainKing;
            _ = _game.Dungeon.TryGetLocation(moveTo, out var place);

            if (!player.EventRecord.ContainsKey(EventIds.CaveOpen))
            {
                player.EventRecord.Add(EventIds.CaveOpen, 1);
            }

            if (!player.EventRecord.ContainsKey(EventIds.Dwarves))
            {
                player.EventRecord.Add(EventIds.Dwarves, 0);
            }

            player.CurrentLocation = place;
            player.ChatClient.PostDirectMessage(player, "Teleported to *" + player.CurrentLocation.Name + "*");

            var numDwarfs = 0;

            foreach (var dwarf in _game.WanderingMonsters)
            {
                if (player.CurrentLocation == dwarf)
                {
                    numDwarfs++;
                }
            }
        }
    }
}
