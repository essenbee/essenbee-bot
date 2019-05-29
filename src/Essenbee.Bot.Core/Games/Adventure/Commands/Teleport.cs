using Essenbee.Bot.Core.Games.Adventure.Events;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Teleport : BaseAdventureCommand
    {

        public Teleport(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var moveTo = Location.SecretEastWestCanyon;
            _ = _game.Dungeon.TryGetLocation(moveTo, out var place);

            if (!player.EventRecord.ContainsKey(EventIds.CaveOpen))
            {
                player.EventRecord.Add(EventIds.CaveOpen, 1);
            }

            player.CurrentLocation = place;
            player.ChatClient.PostDirectMessage(player, "Teleported to *" + player.CurrentLocation.Name + "*");
        }
    }
}
