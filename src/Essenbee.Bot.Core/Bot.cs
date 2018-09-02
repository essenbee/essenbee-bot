using System;
using System.Collections.Generic;
using System.Threading;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using static System.Console;
using System.Linq;
using Essenbee.Bot.Core.Data;
using Essenbee.Bot.Core.Commands;

namespace Essenbee.Bot.Core
{
    public class Bot : IBot
    {
        public List<IChatClient> ConnectedClients { get; } = new List<IChatClient>();
        public IActionScheduler ActionScheduler { get; }
        public IAnswerSearchEngine AnswerSearchEngine { get; }
        public IBotSettings Settings { get; }

        public static readonly string DefaultChannel = "general";
        public ICommandHandler CommandHandler { get; set; }

        private bool _endProgram = false;
        private IRepository _repository;
        
        public Bot(IActionScheduler actionScheduler, IAnswerSearchEngine answerSearchEngine, IConnectedClients clients, IBotSettings settings)
        {
            Settings = settings;
            ActionScheduler = actionScheduler;
            AnswerSearchEngine = answerSearchEngine;
            AnswerSearchEngine.SetApiKey(Settings.AnswerSerachApiKey);
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

            CommandHandler = new BotCommandHandler(this);
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

        private void OnCommandReceived(object sender, ChatCommandEventArgs e)
        {
            foreach (var chatClient in ConnectedClients)
            {
                CommandHandler.ExecuteCommand(chatClient, e);
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