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

        public static bool IsRunning = false;
        private IRepository _repository;
        
        public Bot(IActionScheduler actionScheduler, IAnswerSearchEngine answerSearchEngine, IConnectedClients clients, IBotSettings settings)
        {
            Settings = settings;
            ActionScheduler = actionScheduler;
            AnswerSearchEngine = answerSearchEngine;
            AnswerSearchEngine.SetApiKey(Settings.AnswerSerachApiKey);
            ConnectedClients = clients.ChatClients;
            CommandHandler = new BotCommandHandler(this);
        }

        public void Init(IRepository repository)
        {
            if (_repository is null)
            {
                _repository = repository;
                ScheduleRepeatedMessages();
                ShowStartupMessage();
            }
        }

        public void Start()
        {
            IsRunning = true;
            
            foreach (var chatClient in ConnectedClients)
            {
                chatClient.OnChatCommandReceived += (object sender, ChatCommandEventArgs e) => {
                    ConnectedClients.ForEach(client => CommandHandler.ExecuteCommand(client, e));
                };
            }

            CancelKeyPress += OnCtrlC;

            while (true)
            {
                Thread.Sleep(1000);

                if (!IsRunning) break;
            }
        }

        public void Stop()
        {
            IsRunning = false;
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

        private void OnCtrlC(object sender, ConsoleCancelEventArgs e)
        {
            ConnectedClients.ForEach(client => client.Disconnect());
            IsRunning = false;
        }
    }
}