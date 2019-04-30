using Essenbee.Bot.Core.Commands;
using Essenbee.Bot.Core.Data;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Console;

namespace Essenbee.Bot.Core
{
    public class Bot : IBot
    {
        public List<IChatClient> ConnectedClients { get; } = new List<IChatClient>();
        public IActionScheduler ActionScheduler { get; }
        public IAnswerSearchEngine AnswerSearchEngine { get; }
        public IBotSettings Settings { get; }
        public ICommandHandler CommandHandler { get; set; }
        public Dictionary<string, DateTimeOffset> CommandInvocations { get; } = new Dictionary<string, DateTimeOffset>();

        public static bool IsRunning = false;
        private IRepository _repository;
        private static bool InitialStartup = true;

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
            }
        }

        public void Start()
        {
            IsRunning = true;

            if (InitialStartup)
            {
                // Don't add the event handler(s) more than once!
                foreach (var chatClient in ConnectedClients)
                {
                    chatClient.OnChatCommandReceived += (object sender, ChatCommandEventArgs e) =>
                    {
                        ConnectedClients.ForEach(client => CommandHandler.ExecuteCommand(client, e));
                    };
                }

                InitialStartup = false;
            }

            CancelKeyPress += OnCtrlC;

            while (true)
            {
                Thread.Sleep(1000);

                if (!IsRunning)
                {
                    break;
                }
            }
        }

        public void Stop() => IsRunning = false;

        public void ShowStartupMessage(string startupMessage)
        {
            if (startupMessage != null)
            {
                foreach (var chatClient in ConnectedClients)
                {
                    chatClient.PostMessage(chatClient.DefaultChannel, startupMessage);
                }
            }
        }

        private void ScheduleRepeatedMessages()
        {
            if ((_repository != null) && (ActionScheduler != null))
            {
                var messages = _repository.List<TimedMessage>().Where(m => m.Status == ItemStatus.Active);

                foreach (var message in messages)
                {
                    var action = new RepeatingMessage(message.Message, message.Delay, ConnectedClients, $"AutomatedMessage-{message.Id}");
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