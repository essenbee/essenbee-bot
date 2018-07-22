using System.Collections.Generic;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IBot
    {
        List<IChatClient> ConnectedClients { get; }
        IActionScheduler ActionScheduler { get; }

        void SetRepository(IRepository repository);
        //void SetProjectAnswerKey(string key);
        void Start();
    }
}
