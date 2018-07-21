using System;
using System.Collections.Generic;
using System.Threading;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
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
        private IActionScheduler _actionScheduler;

        public Bot()
        {
            // _autoMessaging = new AutoMessaging(new SystemClock());
        }

        public Bot(List<IChatClient> connectedClients)
        {
            ConnectedClients = connectedClients ?? throw new ArgumentNullException(nameof(connectedClients));
        }

        public void SetRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void SetActionScheduler(IActionScheduler scheduler)
        {
            _actionScheduler = scheduler;
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
            ScheduleRepeatedMessages();
            ShowStartupMessage();

            BeginLoop();
        }

        private void ShowStartupMessage()
        {
            if (_repository != null)
            {
                var startupMessage = _repository.List<StartupMessage>().FirstOrDefault(m => m.Status == ItemStatus.Active);

                if (startupMessage != null)
                {
                    foreach (var client in ConnectedClients)
                    {
                        foreach (var channel in client.Channels)
                        {
                            client.PostMessage(channel.Key,
                                $"{DateTime.Now.ToShortTimeString()} - {startupMessage.Message}");
                        }
                    }
                }
            }
        }

        private void ScheduleRepeatedMessages()
        {
            if (_repository != null && _actionScheduler != null)
            {
                var messages = _repository.List<TimedMessage>().Where(m => m.Status == ItemStatus.Active);

                var channels = ConnectedClients.SelectMany(c => c.Channels);
                var channel = channels.FirstOrDefault(x => x.Key.Equals(DefaultChannel));

                foreach (var message in messages)
                {
                    var action = new RepeatingMessage(channel.Value, message.Message, message.Delay, ConnectedClients, $"AutomatedMessage-{message.Id}");
                    _actionScheduler.Schedule(action);
                }
            }
        }

        public void SetProjectAnswerKey(string key)
        {
            ProjectAnswerKey = key;
        }

        private void BeginLoop()
        {
            while (true)
            {
                Thread.Sleep(1000);

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
