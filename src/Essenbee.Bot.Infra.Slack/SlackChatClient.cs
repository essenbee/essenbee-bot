using System;
using System.Collections.Generic;
using Essenbee.Bot.Core.Interfaces;
using static System.Console;
using SlackLibCore;
using System.Linq;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Infra.Slack
{
    public class SlackChatClient: IChatClient
    {
        private readonly Client _slackClient;
        private  int _connectionFailures = 0;
        private  bool _shutdown = false;
        private bool _isReady = false;
        private readonly SlackConfig _settings;

        public bool UseUsernameForIM { get; }
        public string DefaultChannel => "general";
        public event EventHandler<Core.ChatCommandEventArgs> OnChatCommandReceived = null;
        public IDictionary<string, string> Channels { get; set; } = new Dictionary<string, string>();

        public SlackChatClient(SlackConfig settings)
        {
            _settings = settings;
            _slackClient = new Client(_settings.ApiKey);

            SetupEvents();
            _slackClient.Connect();
        }

        public SlackChatClient(string apiKey)
        {
            _slackClient = new Client(apiKey);

            SetupEvents();
            _slackClient.Connect();
        }

        private void SetupEvents()
        {
            // Wire up basic connectivity events
            _slackClient.ServiceConnected += OnConnected;
            _slackClient.ServiceConnectionFailed += OnDisconnected;
            _slackClient.ServiceDisconnected += OnDisconnected;
            _slackClient.Hello += OnHello;

            // Wire up additional events
            _slackClient.Message += OnMessage;
            _slackClient.MesssageEdit += OnMessageEdit;
            _slackClient.CommandReceived += ProcessCommand;
        }

        public void Disconnect()
        {
            WriteLine("Disconnecting from the Slack service ...");
            _shutdown = true;
            _slackClient.Disconnect();
        }

        public void PostMessage(string theChannel, string msg)
        {
            if (Channels.Values.Contains(theChannel))
            {
                var retries = 0;

                while (retries < 5)
                {
                    if (!_isReady)
                    {
                        System.Threading.Thread.Sleep(2000);
                        retries++;
                    }
                    else
                    {
                        var msgArgs = new Chat.PostMessageArguments {
                            channel = theChannel,
                            text = msg,
                            unfurl_links = false,
                            unfurl_media = false,
                        };

                        _slackClient.Chat.PostMessage(msgArgs);
                        break;
                    }
                }
            }
        }

        public void PostMessage(string msg)
        {
            var retries = 0;

            while (retries < 5)
            {
                if (!_isReady)
                {
                    System.Threading.Thread.Sleep(2000);
                    retries++;
                }
                else
                {
                    foreach (var channel in Channels)
                    {
                        PostMessage(channel.Key, msg);
                    }
                    break;
                }
            }
        }

        public void PostDirectMessage(string userId, string msg)
        {
            var imChannelList = _slackClient.IM.List();
            var userChannel = imChannelList.ims.FirstOrDefault(c => c.user == userId);

            if (userChannel is null) return;

            PostMessage(userChannel.id, msg);
        }

        public void PostDirectMessage(IAdventurePlayer player, string msg)
        {
            if (player is null) return;

            var imChannelList = _slackClient.IM.List();
            var userChannel = imChannelList.ims.FirstOrDefault(c => c.user == player.Id);

            if (userChannel is null) return;

            PostMessage(userChannel.id, msg);
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
            var channels = _slackClient.Channels.List()?.channels ?? new List<RTM.Channel>();

            WriteLine("The following channels exist:");

            foreach (var channel in channels)
            {
                Channels.Add(channel.name, channel.id);
                WriteLine($"\t* {channel.name} ({channel.id})");
            }

            WriteLine();
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
            if (e.user != null)
            {
                var user = e?.UserInfo?.name ?? string.Empty;
                var text = e?.text ?? "<< none >>";

                WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");
            }
        }

        private void OnMessageEdit(MessageEditEventArgs e)
        {
            var user = e?.UserInfo?.name ?? string.Empty;
            var text = e?.message?.text ?? "<< deleted >>";

            WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");
        }

        private void ProcessCommand(CommandEventArgs e)
        {
            var user = e?.UserName ?? "<unknown>";
            var command = e?.Command ?? "<< none >>";
            var args = e?.ArgsAsString ?? string.Empty;
            var argsList = e?.ArgsAsList ?? new List<string>();
            var channel = e?.Channel ?? string.Empty;
            var userId = e?.User ?? string.Empty;
            var clientType = GetType().ToString();

            OnChatCommandReceived?.Invoke(this, new Core.ChatCommandEventArgs(command, argsList, channel, user, userId, clientType));
        }
    }
}
