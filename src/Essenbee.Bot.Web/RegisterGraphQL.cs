using Essenbee.Bot.Infra.GraphQL.Types;
using Essenbee.Bot.Infra.GraphQL;
using GraphQL;
using GraphQL.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Essenbee.Bot.Web
{
    public static class RegisterGraphQL
    {
        public static void Configure(IServiceCollection services, IHostEnvironment env)
        {
            // For GraphQL
            services.AddScoped<IDependencyResolver>(s =>
                new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<BotDataSchema>();
            services.AddScoped<BotDataQuery>();
            services.AddScoped<TimedMessageType>();
            services.AddScoped<ProjectTextType>();

            services.AddGraphQL(options =>
            {
                options.ExposeExceptions = env.IsDevelopment();
            })
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddDataLoader();
        }
    }
}
