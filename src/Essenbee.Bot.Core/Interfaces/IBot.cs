using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IBot
    {
        List<IChatClient> ConnectedClients { get; }
        IActionScheduler ActionScheduler { get; }
        IAnswerSearchEngine AnswerSearchEngine { get; }
        IBotSettings Settings { get; }
        ICommandHandler CommandHandler { get; set; }
        Dictionary<string, DateTimeOffset> CommandInvocations { get; }

        void Start(string message);
        void Stop(string message);
    }
}
