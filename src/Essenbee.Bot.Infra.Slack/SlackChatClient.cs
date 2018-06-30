using System;
using System.Collections.Generic;
using Essenbee.Bot.Core.Interfaces;
using static System.Console;
using Slack;

namespace Essenbee.Bot.Infra.Slack
{
    public class SlackChatClient: IChatClient
    {
        private readonly Client _slackClient;
        private  int _connectionFailures = 0;
        private  bool _shutdown = false;
        private bool _isReady = false;

        public IDictionary<string, string> Channels { get; set; } = new Dictionary<string, string>();

        public SlackChatClient(string apiKey)
        {
            _slackClient = new Client(apiKey);

            // Wire up basic connectivity events
            _slackClient.ServiceConnected += OnConnected;
            _slackClient.ServiceConnectionFailed += OnDisconnected;
            _slackClient.ServiceDisconnected += OnDisconnected;
            _slackClient.Hello += OnHello;
            _slackClient.PongReceived += OnPong;

            // Wire up additional events
            _slackClient.Message += OnMessage;
            _slackClient.MesssageEdit += OnMessageEdit;

            _slackClient.Connect();
        }

        public void Disconnect()
        {
            WriteLine("Disconnecting from the Slack service ...");
            _shutdown = true;
            _slackClient.Disconnect();
        }

        public void PostMessage(string theChannel, string msg)
        {
            if (_isReady)
            {
                var msgArgs = new Chat.PostMessageArguments
                {
                    channel = theChannel,
                    text = msg
                };

                _slackClient.Chat.PostMessage(msgArgs);
            }
            else
            {
                Console.WriteLine("Slack Chat Client is not connected to the Slack service!");
            }
        }

        private void OnConnected()
        {
            _isReady = true;
            _connectionFailures = 0;
            WriteLine("Connected to the Slack service");
        }

        private void OnHello(HelloEventArgs e)
        {
            WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tHello");
            var channels = _slackClient.Channels.List()?.channels ?? new List<RTM.channel>();

            WriteLine("The following channels exist:");

            foreach (var channel in channels)
            {
                Channels.Add(channel.name, channel.id);
                WriteLine($"\t* {channel.name} ({channel.id})");
            }

            WriteLine();
        }

        private void OnPong()
        {
            _connectionFailures = 0;
            WriteLine("--> Pong received!");
        }

        private void OnDisconnected()
        {
            if (_shutdown)
            {
                WriteLine("Disconnected from Slack (shutdown).");
                return;
            }

            try
            {
                _connectionFailures++;
                System.Threading.Thread.Sleep(_connectionFailures < 13 ? 5000 : 60_000);
                WriteLine($"Attempting to reconnect to the Slack service. Attempt {_connectionFailures}");
                _slackClient.Connect();
            }
            catch (Exception ex)
            {
                WriteLine($"Unable to handle service disconnection.\r\n{ex.Message}\r\n{ex.StackTrace}");
            }
        }

        private void OnMessage(MessageEventArgs e)
        {
            if (e.user == null)
            {
                return;
            }

            var user = e?.UserInfo?.name ?? string.Empty;
            var text = e?.text ?? "<< none >>";

            WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");
            // ProcessCommands(e.UserInfo.name, e.channel, e.text);
        }

        private void OnMessageEdit(MessageEditEventArgs e)
        {
            var user = e?.UserInfo?.name ?? string.Empty;
            var text = e?.message?.text ?? "<< deleted >>";

            WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");
            // ProcessCommands(e.UserInfo.name, e.channel, e.text);
        }
    }
}
