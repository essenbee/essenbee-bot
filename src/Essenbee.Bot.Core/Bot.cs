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
        public IBotClient BotDataClient { get; }
        public IActionScheduler ActionScheduler { get; }
        public IAnswerSearchEngine AnswerSearchEngine { get; }
        public IBotSettings Settings { get; }
        public ICommandHandler CommandHandler { get; set; }
        public Dictionary<string, DateTimeOffset> CommandInvocations { get; } = new Dictionary<string, DateTimeOffset>();

        public static bool IsRunning = false;
        private static bool InitialStartup = true;

        public Bot(IActionScheduler actionScheduler, IAnswerSearchEngine answerSearchEngine, IConnectedClients clients, 
            IBotClient botDataClient, IBotSettings settings)
        {
            Settings = settings;
            ActionScheduler = actionScheduler;
            AnswerSearchEngine = answerSearchEngine;
            BotDataClient = botDataClient;
            AnswerSearchEngine.SetApiKey(Settings.AnswerSearchApiKey);
            ConnectedClients = clients.ChatClients;
            CommandHandler = new BotCommandHandler(this);
        }

        public void Start(string startupMessage)
        {
            IsRunning = true;

            if (InitialStartup)
            {
                AddCommandHandler();
                CancelKeyPress += OnCtrlC;
                InitialStartup = false;
            }

            ShowStartupMessage(startupMessage);

            while (true)
            {
                Thread.Sleep(1000);

                if (!IsRunning)
                {
                    break;
                }
            }
        }

        public void Stop(string stopMessage)
        {
            if (!string.IsNullOrWhiteSpace(stopMessage))
            {
                foreach (var chatClient in ConnectedClients)
                {
                    chatClient.PostMessage(chatClient.DefaultChannel, stopMessage);
                }
            }

            IsRunning = false;
        }

        public void ScheduleRepeatedMessages(IActionScheduler actionScheduler, IRepository repository)
        {
            if ((repository != null) && (actionScheduler != null))
            {
                var messages = repository.List<TimedMessage>().Where(m => m.Status == ItemStatus.Active);

                foreach (var message in messages)
                {
                    var action = new RepeatingMessage(message.Message, message.Delay, ConnectedClients, $"AutomatedMessage-{message.Id}");
                    actionScheduler.Schedule(action);
                }
            }
        }

        private void AddCommandHandler()
        {
            foreach (var chatClient in ConnectedClients)
            {
                chatClient.OnChatCommandReceived += (object sender, ChatCommandEventArgs e) =>
                {
                    CommandHandler.ExecuteCommand(chatClient, e);
                    //ConnectedClients.ForEach(client => CommandHandler.ExecuteCommand(client, e));
                };
            }
        }

        private void ShowStartupMessage(string startupMessage)
        {
            if (!string.IsNullOrWhiteSpace(startupMessage))
            {
                foreach (var chatClient in ConnectedClients)
                {
                    chatClient.PostMessage(chatClient.DefaultChannel, startupMessage);
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