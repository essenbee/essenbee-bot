using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class WanderingMonster
    {
        public IAdventureLocation CurrentLocation { get; set; }
        public IAdventureLocation PrevLocation { get; set; }
        public bool IsEnraged { get; set; }
        public bool HasSeenPlayer { get; set; }

        public WanderingMonster(IAdventureLocation location)
        {
            CurrentLocation = location;
            PrevLocation = location;
        }
    }
}
