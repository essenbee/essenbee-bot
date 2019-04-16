using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Interfaces;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;
using static System.Console;

namespace Essenbee.Bot.Infra.Twitch
{
    public class TwitchChatClient : IChatClient
    {
        public event EventHandler<ChatCommandEventArgs> OnChatCommandReceived;
        public bool UseUsernameForIM { get; }
        public string DefaultChannel { get; } = string.Empty;
        public IDictionary<string, string> Channels { get; set; } = new Dictionary<string, string>();

        private readonly TwitchClient _twitchClient;
        private readonly TwitchConfig _settings;
        private int _connectionFailures = 0;
        private const int MaxMsgSize = 500;

        public TwitchChatClient(TwitchConfig settings)
        {
            _settings = settings;
            var credentials = new ConnectionCredentials(settings.Username, settings.Token);
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, channel: settings.Channel);

            UseUsernameForIM = true;
            DefaultChannel = _settings.Channel;

            SetupEvents();
            _twitchClient.Connect();
        }

        public TwitchChatClient(string username, string token, string channel)
        {
            var credentials = new ConnectionCredentials(username, token);
            _twitchClient = new TwitchClient();
            _twitchClient.Initialize(credentials, channel: channel);
            _settings = new TwitchConfig {
                Username = username,
                Token = token,
                Channel = channel
            };

            UseUsernameForIM = true;
            DefaultChannel = _settings.Channel;

            SetupEvents();
            _twitchClient.Connect();
        }

        private void SetupEvents()
        {
            // Wire up basic connectivity events
            _twitchClient.OnConnected += OnConnected;
            _twitchClient.OnConnectionError += OnConnectionError;
            _twitchClient.OnDisconnected += OnDisconnected;
            _twitchClient.OnLog += OnLog;

            // Wire up additional events
            _twitchClient.OnJoinedChannel += OnJoinedChannel;
            _twitchClient.OnMessageReceived += OnMessage;
            _twitchClient.OnChatCommandReceived += ProcessCommand;
            _twitchClient.OnWhisperCommandReceived += ProcessCommand;
        }

        public void PostMessage(string channel, string text) => PostMessage(text);

        public void PostMessage(string text)
        {
            if (_twitchClient.IsConnected && _twitchClient.JoinedChannels.Count > 0)
            {
                var messageParts = SplitToLines(text, MaxMsgSize);

                foreach (var part in messageParts)
                {
                    _twitchClient.SendMessage(_settings.Channel, part);
                }
            }
        }

        public void PostDirectMessage(string username, string text)
        {
            if (_twitchClient.IsConnected && _twitchClient.JoinedChannels.Count > 0)
            {
                var messageParts = SplitToLines(text, MaxMsgSize);

                foreach (var part in messageParts)
                {
                    _twitchClient.SendWhisper(username, part);
                }
            }
        }

        public void PostDirectMessage(IAdventurePlayer player, string text) => PostDirectMessage(player.UserName, text);
        
        public void Disconnect()
        {
            WriteLine("Disconnecting from the Twitch service ...");
            _twitchClient.Disconnect();
        }

        private void OnLog(object sender, OnLogArgs e)
        {
            WriteLine($"{e.DateTime.ToString(CultureInfo.InvariantCulture)}: {e.BotUsername} - {e.Data}");
        }

        private void OnConnected(object sender, OnConnectedArgs e)
        {
            var botName = e?.BotUsername ?? "<unknown>";
            _connectionFailures = 0;
            WriteLine($"{botName} is connected to Twitch!");
        }

        private void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            // WriteLine("AlphaBot joined the channel!");
            // _twitchClient.SendMessage(e.Channel, "AlphaBot joined the channel!");
        }

        private void OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            WriteLine("Disconnected from Twitch!");
        }

        private void OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            var botName = e?.BotUsername ?? "<unknown>";
            _connectionFailures++;
            WriteLine($"{botName} had a problem connecting to Twitch (failure #{_connectionFailures})!");
        }

        private void OnMessage(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.UserId == null)
            {
                return;
            }

            var user = e?.ChatMessage?.Username ?? string.Empty;
            var text = e?.ChatMessage?.Message ?? "<< none >>";

            WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");
        }

        private void ProcessCommand(object sender, OnChatCommandReceivedArgs e)
        {
                var user = e?.Command?.ChatMessage?.DisplayName ?? "<unknown>";
                var command = e?.Command?.CommandText ?? "<none>";
                var args = e?.Command?.ArgumentsAsString ?? string.Empty;
                var argsList = e?.Command?.ArgumentsAsList ?? new List<string>();
                var channel = string.Empty;
                var userId = e?.Command?.ChatMessage?.UserId ?? string.Empty;
                var clientType = GetType().ToString();

                var userRole = GetRole(e?.Command?.ChatMessage);

                OnChatCommandReceived?.Invoke(this,
                    new ChatCommandEventArgs(command, argsList, channel, user, userId, clientType, userRole));
        }

        private UserRole GetRole(ChatMessage chatMessage)
        {
            if (chatMessage.IsBroadcaster) return UserRole.Streamer;
            if (chatMessage.IsModerator) return UserRole.Moderator;
            return chatMessage.IsSubscriber ? UserRole.Subscriber : UserRole.Viewer;
        }

        private void ProcessCommand(object sender, OnWhisperCommandReceivedArgs e)
        {
            var user = e?.Command?.WhisperMessage?.DisplayName ?? "<unknown>";
            var command = e?.Command?.CommandText ?? "<none>";
            var args = e?.Command?.ArgumentsAsString ?? string.Empty;
            var argsList = e?.Command?.ArgumentsAsList ?? new List<string>();
            var channel = string.Empty;
            var userId = e?.Command?.WhisperMessage?.UserId ?? string.Empty;
            var clientType = GetType().ToString();

            OnChatCommandReceived?.Invoke(this, new ChatCommandEventArgs(command, argsList, channel, user, userId, clientType));
        }

        private static IEnumerable<string> SplitToLines(string value, int maximumLineLength)
        {
            var words = value.Split(' ');
            var line = new StringBuilder();

            foreach (var word in words)
            {
                if ((line.Length + word.Length) >= maximumLineLength)
                {
                    yield return line.ToString();
                    line = new StringBuilder();
                }

                line.AppendFormat("{0}{1}", (line.Length > 0) ? " " : "", word);
            }

            yield return line.ToString();
        }
    }
}
