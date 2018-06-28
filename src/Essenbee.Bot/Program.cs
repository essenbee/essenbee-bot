using System;
using System.IO;
using System.Threading;
using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Messaging;
using Essenbee.Bot.Core.Utilities;
using Microsoft.Extensions.Configuration;
using static System.Console;

namespace Essenbee.Bot
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            WriteLine("CoreBot is getting ready....");
            WriteLine("Press [Ctrl]+C to exit.");

            var autoMessaging = new AutoMessaging(new SystemClock());

            var testMsg = new TimerTriggeredMessage
            {
                Delay = 1, // Minutes
                Message ="Hi, this is a timed message from CoreBot!"
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
                    WriteLine($"{DateTime.Now.ToShortTimeString()} - {result.message}");
                }
            }
        }
    }
}
