using System;
using System.Collections.Generic;
using System.Threading;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using Essenbee.Bot.Core.Utilities;
using static System.Console;

namespace Essenbee.Bot.Core
{
    public class Bot
    {
        public List<IChatClient> ConnectedClients { get; }

        private static bool _endProgram = false;

        public Bot(List<IChatClient> connectedClients)
        {
            ConnectedClients = connectedClients;
        }

        public void Start()
        {
            Console.CancelKeyPress += OnCtrlC;
            var defaultChannel = "general";

            WriteLine();
            WriteLine("{}oo((X))ΞΞΞΞΞΞΞΞΞΞΞΞΞ>");
            WriteLine();
            WriteLine("Press [Ctrl]+C to exit.");
            WriteLine();

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

                    foreach (var client in ConnectedClients)
                    {
                        var channelId = client.Channels.ContainsKey(defaultChannel) ?
                            client.Channels[defaultChannel]
                            : string.Empty;

                        client.PostMessage(channelId,
                            $"{DateTime.Now.ToShortTimeString()} - {result.message}");
                    }
                }

                if (_endProgram) break;
            }
        }

        private void OnCtrlC(object sender, ConsoleCancelEventArgs e)
        {
            foreach (var client in ConnectedClients)
            {
                client.Disconnect();
            }

            _endProgram = true;
        }
    }
}
