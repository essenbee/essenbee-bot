using Essenbee.Bot.Core.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public class AdventurePlayer
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public AdventureLocation CurrentLocation { get; set; }
        public IChatClient ChatClient { get; set; }
        public int Score { get; set; }
        public int Moves { get; set; }
    }
}
