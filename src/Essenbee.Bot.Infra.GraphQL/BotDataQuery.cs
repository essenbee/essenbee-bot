using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Data;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.GraphQL.Types;
using GraphQL.Types;
using System.Linq;

namespace Essenbee.Bot.Infra.GraphQL
{
    public class BotDataQuery : ObjectGraphType
    {
        private readonly IRepository _repo;

        public BotDataQuery(IRepository repo)
        {
            _repo = repo;

            Field<ListGraphType<TimedMessageType>>("repeatedMessages",
                resolve: ctx =>
                {
                    return _repo.List<TimedMessage>()
                        .Where(m => m.Status == ItemStatus.Active);
                });
        }
    }
}
