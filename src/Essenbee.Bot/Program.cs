using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using Essenbee.Bot.Core.Utilities;
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
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");


            if (isDevelopment) // Only add secrets in development
            {
                builder.AddUserSecrets<UserSecrets>();
            }

            Configuration = builder.Build();

            WriteLine("CoreBot is getting ready....");

            // Make UserSecrets injectable ...
            var services = new ServiceCollection()
                .Configure<UserSecrets>(Configuration.GetSection(nameof(UserSecrets)))
                .AddOptions()
                .AddSingleton<UserSecretsProvider>()
                .BuildServiceProvider();

            services.GetService<UserSecrets>();

            var secrets = services.GetService<UserSecretsProvider>().Secrets;
            var generalChannel = "C9QT99U2D";

            WriteLine("Press [Ctrl]+C to exit.");

            var autoMessaging = new AutoMessaging(new SystemClock());

            var testMsg = new TimerTriggeredMessage
            {
                Delay = 1, // Minutes
                Message = "Hi, this is a timed message from CoreBot!"
            };

            autoMessaging.PublishMessage(testMsg);

            // Handle multiple chat clients that implement IChatClient ...
            var connectedChatClients = ConnectChatClients(secrets);

            while (true)
            {
                Thread.Sleep(1000);

                // Show Timer Triggered Messages
                autoMessaging.EnqueueMessagesToDisplay();

                while (true)
                {
                    var result = autoMessaging.DequeueNextMessage();

                    if (!result.isMessage) break;

                    foreach (var client in connectedChatClients)
                    {
                        client.PostMessage(generalChannel,
                            $"{DateTime.Now.ToShortTimeString()} - {result.message}");
                    }
                }
            }
        }

        private static List<IChatClient> ConnectChatClients(UserSecrets secrets)
        {
            var slackApiKey = secrets.SlackApiKey;

            return new List<IChatClient>
            {
                new ConsoleChatClient(),
                new SlackChatClient(slackApiKey),
            };
        }
    }
}
