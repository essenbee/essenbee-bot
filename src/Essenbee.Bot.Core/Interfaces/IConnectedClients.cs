using System.Collections.Generic;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IConnectedClients
    {
        List<IChatClient> ChatClients { get; }
    }
}
