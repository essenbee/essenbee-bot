using Essenbee.Bot.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IBotClient
    {
        Task<List<TimedMessageModel>> GetTimedMessages();
        Task<ProjectTextModel> GetProjectText();
    }
}
