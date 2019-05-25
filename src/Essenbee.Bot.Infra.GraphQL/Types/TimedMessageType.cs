using Essenbee.Bot.Core.Data;
using GraphQL.Types;

namespace Essenbee.Bot.Infra.GraphQL.Types
{
    public class TimedMessageType : ObjectGraphType<TimedMessage>
    {
        public TimedMessageType()
        {
            Field(f => f.Message);
            Field(f => f.Delay);
        }
    }
}
