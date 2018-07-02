using System;
using System.Collections.Generic;
using System.Threading;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using Essenbee.Bot.Core.Utilities;
using Essenbee.Bot.Core.Commands;
using static System.Console;
using System.Linq;

namespace Essenbee.Bot.Core
{
    public class Bot
    {
        public List<IChatClient> ConnectedClients { get; }

        public static readonly string DefaultChannel = "general";
        public static readonly Dictionary<string, ICommand> _CommandsAvailable = new Dictionary<string, ICommand>();

        private bool _endProgram = false;
        private readonly AutoMessaging _autoMessaging;

        public Bot(List<IChatClient> connectedClients)
        {
            ConnectedClients = connectedClients ?? throw new ArgumentNullException(nameof(connectedClients));
            _autoMessaging = new AutoMessaging(new SystemClock());

            foreach (var chatClient in connectedClients)
            {
                chatClient.OnChatCommandReceived += OnCommandReceived;
            }
        }

        public void Start()
        {
            CancelKeyPress += OnCtrlC;

            WriteLine();
            WriteLine("Press [Ctrl]+C to exit.");
            WriteLine();

            LoadCommands();
            PublishTimerTriggeredMessages();
            BeginLoop();
        }

        private void PublishTimerTriggeredMessages()
        {
            // ToDo: Eventally, we will want to get these messages from a datastore ...
            var testMsg = new TimerTriggeredMessage
            {
                Delay = 1, // Minutes
                Message = "Hi, this is a timed message from CoreBot!"
            };

            _autoMessaging.PublishMessage(testMsg); // Draft message by default

            // Activate the message
            testMsg.Status = ItemStatus.Active;
        }

        private void BeginLoop()
        {
            while (true)
            {
                Thread.Sleep(1000);

                // Show Timer Triggered Messages
                _autoMessaging.EnqueueMessagesToDisplay();

                while (true)
                {
                    var (isMessage, message) = _autoMessaging.DequeueNextMessage();

                    if (!isMessage) break;

                    foreach (var client in ConnectedClients)
                    {
                        var channelId = client.Channels.ContainsKey(DefaultChannel)
                            ? client.Channels[DefaultChannel]
                            : string.Empty;

                        client.PostMessage(channelId,
                            $"{DateTime.Now.ToShortTimeString()} - {message}");
                    }
                }

                if (_endProgram) break;
            }
        }

        private void LoadCommands()
        {
            if (_CommandsAvailable.Count > 0)
            {
                return;
            }

            var commandTypes = GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICommand)));

            foreach (var type in commandTypes)
            {
                if (type.Name == "ICommand") continue;

                var cmd = Activator.CreateInstance(type) as ICommand;
                cmd.Status = ItemStatus.Active;
                _CommandsAvailable.Add(cmd.CommandName, cmd);
            }
        }

        private void OnCommandReceived(object sender, ChatCommandEventArgs e)
        {
            foreach (var chatClient in ConnectedClients)
            {
                // Check command name against available commands ...
                if (_CommandsAvailable.TryGetValue(e.Command, out ICommand cmd))
                {
                    cmd.Execute(chatClient, e);
                }
                else
                {
                    chatClient.PostMessage(e.Channel, $"The command {e.Command} has not been implemented.");
                }
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
