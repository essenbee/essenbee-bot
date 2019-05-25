using GraphQL;
using GraphQL.Types;

namespace Essenbee.Bot.Infra.GraphQL
{
    public class BotDataSchema : Schema
    {
        public BotDataSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<BotDataQuery>();
        }
    }
}
