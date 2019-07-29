using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class WanderingMonster
    {
        public IAdventureLocation CurrentLocation { get; set; }
        public IAdventureLocation PrevLocation { get; set; }
        public bool IsEnraged { get; set; }
        public bool HasSeenPlayer { get; set; }
        public MonsterGroup Group { get; set; }

        public WanderingMonster(IAdventureLocation location, MonsterGroup group)
        {
            CurrentLocation = location;
            PrevLocation = location;
            Group = group;
        }
    }

    public enum MonsterGroup
    {
        Dwarves,
        Pirate,
    }
}
