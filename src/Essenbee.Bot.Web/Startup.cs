using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.CognitiveServices;
using Essenbee.Bot.Infra.Discord;
using Essenbee.Bot.Infra.Hangfire;
using Essenbee.Bot.Infra.Slack;
using Essenbee.Bot.Infra.Twitch;
using Essenbee.Bot.Infra.GraphQL;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Essenbee.Bot.Clients.GraphQL;
using Hangfire.Logging.LogProviders;
using Microsoft.Extensions.Hosting;
using TwitchLib.Api;

namespace Essenbee.Bot.Web
{
    //`
    //` ![](BECF8D6358A570AF7A626A1C8EC9D254.png;;;0.00917,0.00839)

    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // If using Kestrel:
            //services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.AllowSynchronousIO = true;
            //});

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<UserSecrets>(Configuration.GetSection(nameof(UserSecrets)));
            var secrets = Configuration.GetSection(nameof(UserSecrets)).Get<UserSecrets>();
            services.Configure<ConnectedClientSettings>(Configuration.GetSection("ChatClients"));

            // Injected into ChatClients by DI for Hangfire scheduled actions
            var slackConfig = new SlackConfig 
            {
                ApiKey = secrets.SlackApiKey,
                DefaultChannel = Configuration["ChatClients:SlackDefaultChannel"],
            };
            var discordConfig = new DiscordConfig 
            {
                DiscordToken = secrets.DiscordToken,
                DefaultChannel = Configuration["ChatClients:DiscordDefaultChannel"],
            };
            var twitchConfig = new TwitchConfig {
                Username = secrets.TwitchUsername,
                Token = secrets.TwitchToken,
                Channel = secrets.TwitchChannel,
                ClientId = secrets.TwitchClientId,
                BaseApiUrl = secrets.BaseTwitchApiUrl,
            };

            services.AddSingleton(slackConfig);
            services.AddSingleton(discordConfig);
            services.AddSingleton(twitchConfig);

            services.AddSingleton<IConnectedClients, ConnectedClients>();
            services.AddSingleton<IBotSettings, BotSettings>();
            services.AddSingleton<IBotClient, BotGraphClient>();

            //services.AddHangfire(x => x.UseSqlServerStorage(secrets.DatabaseConnectionString));

            services.AddHangfire(config => config
                .UseLogProvider(new ColouredConsoleLogProvider())
                .UseSqlServerStorage(secrets.DatabaseConnectionString));

            services.AddDbContext<AppDataContext>(options =>
                options.UseSqlServer(secrets.DatabaseConnectionString));

            services.AddScoped<IRepository, EntityFrameworkRepository>();
            services.AddSingleton<IActionScheduler, HangfireActionScheduler>();
            services.AddSingleton<IAnswerSearchEngine, AnswerSearch>();
            services.AddSingleton<IBot, Core.Bot>();

            RegisterGraphQL.Configure(services, _env);

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(options => options.NoReferrer());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXfo(options => options.Deny());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseGraphQL<BotDataSchema>(path: "/graphql");
            if (env.IsDevelopment())
            {
                app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
                {
                    Path = "/ui/playground"
                });
            }

            app.UseMvc();
        }
    }
}
