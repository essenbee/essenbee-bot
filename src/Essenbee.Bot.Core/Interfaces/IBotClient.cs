using System.Threading.Tasks;

namespace Essenbee.Bot.Core.Interfaces
{
    public interface IBotClient
    {
        Task<TimedMessageModel> GetTimedMessages();
    }
}
