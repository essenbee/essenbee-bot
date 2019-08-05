using Essenbee.Bot.Core.Games.Adventure.Events;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Interactions
{
    public class AddPlayerStatus : IAction
    {
        private PlayerStatus _status;

        public AddPlayerStatus(PlayerStatus status)
        {
            _status = status;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item = null)
        {
            if (player.Statuses.Contains(_status)) return false;

            player.Statuses.Add(_status);
            return true;
        }
    }

    public class UpdateEventRecord : IAction
    {
        private EventIds _eventId;
        private int _value;

        public UpdateEventRecord(EventIds eventId, int value)
        {
            _eventId = eventId;
            _value = value;
        }

        public bool Do(IAdventurePlayer player, IAdventureItem item = null)
        {
            if (!player.EventRecord.ContainsKey(_eventId)) return false;

            player.EventRecord[_eventId] += _value;
            return true;
        }
    }
}
