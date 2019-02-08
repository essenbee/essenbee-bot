using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;

namespace Essenbee.Bot.Core.Games.Adventure.Commands
{
    public class Plugh : BaseAdventureCommand
    {
        public Plugh(IReadonlyAdventureGame game, params string[] verbs) : base(game, verbs)
        {
        }

        public override void Invoke(IAdventurePlayer player, ChatCommandEventArgs e)
        {
            var canMove = false;

            if (player.CurrentLocation.LocationId.Equals(Location.Building))
            {
                var moveTo = Location.Y2;
                canMove = _game.Dungeon.TryGetLocation(moveTo, out var place);
                player.CurrentLocation = place;
            }
            else if (player.CurrentLocation.LocationId.Equals(Location.Y2))
            {
                var moveTo = Location.Building;
                canMove = _game.Dungeon.TryGetLocation(moveTo, out var place);
                player.CurrentLocation = place;
            }

            if (canMove)
            {
                player.ChatClient.PostDirectMessage(player, "You suddenly feel nauseous as space seems to fold around you!");
                return;
            }

            player.ChatClient.PostDirectMessage(player, "Nothing happens...");
        }
    }
}
