using System;
using System.Collections.Generic;
using System.Threading;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using Essenbee.Bot.Core.Utilities;
using static System.Console;
using System.Linq;
using Essenbee.Bot.Core.Data;

namespace Essenbee.Bot.Core
{
    public class Bot
    {
        public List<IChatClient> ConnectedClients { get; set; } = new List<IChatClient>();

        public static string ProjectAnswerKey;

        public static readonly string DefaultChannel = "general";
        public static readonly Dictionary<string, ICommand> _CommandsAvailable = new Dictionary<string, ICommand>();

        private bool _endProgram = false;
        private IRepository _repository;
        private readonly AutoMessaging _autoMessaging;

        public Bot()
        {
            _autoMessaging = new AutoMessaging(new SystemClock());
        }

        public Bot(List<IChatClient> connectedClients)
        {
            ConnectedClients = connectedClients ?? throw new ArgumentNullException(nameof(connectedClients));
            _autoMessaging = new AutoMessaging(new SystemClock());
        }

        public void SetRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void SetChatClients(List<IChatClient> connectedClients)
        {
            ConnectedClients = connectedClients ?? throw new ArgumentNullException(nameof(connectedClients));
        }

        public void Start()
        {
            foreach (var chatClient in ConnectedClients)
            {
                chatClient.OnChatCommandReceived += OnCommandReceived;
            }

            CancelKeyPress += OnCtrlC;

            LoadCommands();
            PublishTimerTriggeredMessages();

            ShowStartupMessage();

            BeginLoop();
        }

        private void ShowStartupMessage()
        {
            if (_repository != null)
            {
                //var startupMessage = _repository.Single<StartupMessage>();
                //foreach (var client in ConnectedClients)
                //{
                //    foreach (var channel in client.Channels)
                //    {
                //        client.PostMessage(channel.Key,
                //            $"{DateTime.Now.ToShortTimeString()} - {startupMessage.Message}");
                //    }
                //}
            }
        }

        public void SetProjectAnswerKey(string key)
        {
            ProjectAnswerKey = key;
        }

        private void PublishTimerTriggeredMessages()
        {
            if (_repository != null)
            {
                var timerTriggeredMessages = _repository.List<TimedMessage>();

                foreach (var msg in timerTriggeredMessages)
                {
                    var ttm = new TimerTriggeredMessage(msg.Message, msg.Delay, DateTime.Now);
                    _autoMessaging.PublishMessage(ttm, msg.Status);
                }
            }
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
