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
    public class Bot : IBot
    {
        public List<IChatClient> ConnectedClients { get; } = new List<IChatClient>();
        public IActionScheduler ActionScheduler { get; }
        public IAnswerSearchEngine AnswerSearchEngine { get; }
        public IBotSettings Settings { get; }

        public static readonly string DefaultChannel = "general";
        public static readonly Dictionary<string, ICommand> _CommandsAvailable = new Dictionary<string, ICommand>();

        private bool _endProgram = false;
        private IRepository _repository;
        
        public Bot(IActionScheduler actionScheduler, IAnswerSearchEngine answerSearchEngine, IConnectedClients clients, IBotSettings settings)
        {
            ActionScheduler = actionScheduler;
            AnswerSearchEngine = answerSearchEngine;
            Settings = settings;
            ConnectedClients = clients.ChatClients;
        }

        public void SetRepository(IRepository repository)
        {
            _repository = repository;
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
            if (_repository != null && ActionScheduler != null)
            {
                var startupMessage = _repository.List<StartupMessage>().FirstOrDefault(m => m.Status == ItemStatus.Active);

                if (startupMessage != null)
                {
                    var action = new DelayedMessage(startupMessage.Message, 5, ConnectedClients, $"AutomatedMessage-{startupMessage.Id}");
                    ActionScheduler.Schedule(action);
                }
            }
        }

        private void ScheduleRepeatedMessages()
        {
            if (_repository != null && ActionScheduler != null)
            {
                var messages = _repository.List<TimedMessage>().Where(m => m.Status == ItemStatus.Active);

                // ToDo: Allow configurable channel selection per message
                foreach (var message in messages)
                {
                    var action = new RepeatingMessage(DefaultChannel, message.Message, message.Delay, ConnectedClients, $"AutomatedMessage-{message.Id}");
                    ActionScheduler.Schedule(action);
                }
            }
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

                var cmd = type.Name.Equals("AskCommand") 
                    ? Activator.CreateInstance(type, Settings, AnswerSearchEngine) as ICommand
                    : Activator.CreateInstance(type, Settings) as ICommand;
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
