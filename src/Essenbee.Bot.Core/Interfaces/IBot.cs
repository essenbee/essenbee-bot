using System.Collections.Generic;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IBot
    {
        List<IChatClient> ConnectedClients { get; }
        IActionScheduler ActionScheduler { get; }
        IAnswerSearchEngine AnswerSearchEngine { get; }
        IBotSettings Settings { get; }

        void SetRepository(IRepository repository);
        void Start();
    }
}
