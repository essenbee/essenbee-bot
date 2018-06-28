using System;
using System.IO;
using System.Threading;
using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Messaging;
using Essenbee.Bot.Core.Utilities;
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

            var secrets = services.GetService<UserSecretsProvider>();

            var consoleClient = new ConsoleChatClient();

            WriteLine("Press [Ctrl]+C to exit.");

            var autoMessaging = new AutoMessaging(new SystemClock());

            var testMsg = new TimerTriggeredMessage
            {
                Delay = 1, // Minutes
                Message = "Hi, this is a timed message from CoreBot!"
            };

            autoMessaging.PublishMessage(testMsg);

            while (true)
            {
                Thread.Sleep(1000);

                // Show Timer Triggered Messages
                autoMessaging.EnqueueMessagesToDisplay();

                while (true)
                {
                    var result = autoMessaging.DequeueNextMessage();

                    if (!result.isMessage) break;

                    consoleClient.PostMessage(string.Empty, 
                        $"{DateTime.Now.ToShortTimeString()} - {result.message}");
                }
            }
        }
    }
}
