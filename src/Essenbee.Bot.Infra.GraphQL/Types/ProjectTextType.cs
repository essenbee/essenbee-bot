using Essenbee.Bot.Core.Data;
using GraphQL.Types;

namespace Essenbee.Bot.Infra.GraphQL.Types
{
    public class ProjectTextType : ObjectGraphType<ProjectText>
    {
        public ProjectTextType()
        {
            Field(f => f.Text);
        }
    }
}
