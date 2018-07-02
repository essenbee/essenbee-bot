using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.Slack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Console;

namespace Essenbee.Bot
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            try
            {
                var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
                var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                    devEnvironmentVariable.ToLower() == "development";

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                // Only use secrets in Development; in Production, use appsettings.json
                if (isDevelopment)
                {
                    builder.AddUserSecrets<UserSecrets>();
                }

                Configuration = builder.Build();

                // Make UserSecrets injectable ...
                var services = new ServiceCollection()
                    .Configure<UserSecrets>(Configuration.GetSection(nameof(UserSecrets)))
                    .AddOptions()
                    .AddSingleton<UserSecretsProvider>()
                    .BuildServiceProvider();

                services.GetService<UserSecrets>();

                var secrets = services.GetService<UserSecretsProvider>().Secrets;

                var bot = new Core.Bot(ConnectChatClients(secrets));
                bot.SetProjectAnswerKey(secrets.ProjectAnswerKey);

                WriteLine("CoreBot is getting ready....");
                bot.Start();
            }
            catch (Exception ex)
            {
                WriteLine($"EXCEPTION {ex.Message} - {ex.StackTrace}");
            }
        }

        // ToDo: Remove dependency on the SlackChatClient here ...
        private static List<IChatClient> ConnectChatClients(UserSecrets secrets)
        {
            var connectedClients = new List<IChatClient>
            {
                new ConsoleChatClient(),
                new SlackChatClient(secrets.SlackApiKey),
            };

            Thread.Sleep(5000);

            return connectedClients;
        }
    }
}
