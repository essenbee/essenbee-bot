using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

namespace Essenbee.Bot.Infra.Discord
{
    public class DiscordChatClient : IChatClient
    {
        private readonly DiscordClient _discordClient;
        private readonly DiscordConfig _settings;
        private bool _isReady = false;

        public bool UseUsernameForIM { get; }
        public string DefaultChannel => "general";
        public const ulong General = 546412212038795307;
        public event EventHandler<Core.ChatCommandEventArgs> OnChatCommandReceived = null;
        public IDictionary<string, string> Channels { get; set; } = new Dictionary<string, string>();
        public CommandsNextModule Commands { get; set; }

        public DiscordChatClient(string token)
        {
            _discordClient = new DiscordClient(new DiscordConfiguration {
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            });

            UseUsernameForIM = false;
            SetupEvents();
            SetupDiscordCommands();
            Connect();
        }

        public DiscordChatClient(DiscordConfig settings)
        {
            _settings = settings;
            _discordClient = new DiscordClient(new DiscordConfiguration {
                Token = _settings.DiscordToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            });

            UseUsernameForIM = false;
            SetupEvents();
            SetupDiscordCommands();
            Connect();
        }

        public void Connect()
        {
            _ = ConnectAsync();
        }

        public async Task ConnectAsync()
        {
            await _discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        public void Disconnect()
        {
            WriteLine("Disconnecting from the Discord service ...");
            _discordClient.DisconnectAsync();
            _isReady = false;
        }

        public void PostDirectMessage(string username, string text)
        {
            var found = ulong.TryParse(username, out var userId);

            if (found)
            {
                var user = _discordClient.GetUserAsync(userId).Result;
                var privateChannel = _discordClient.CreateDmAsync(user).Result;
                var channelId = privateChannel.Id.ToString();
                PostMessage(channelId, text);
            }
        }

        public void PostDirectMessage(IAdventurePlayer player, string text)
        {
            var found = ulong.TryParse(player.Id, out var userId);

            if (found)
            {
                var user = _discordClient.GetUserAsync(userId).Result;
                var privateChannel = _discordClient.CreateDmAsync(user).Result;
                var channelId = privateChannel.Id.ToString();
                PostMessage(channelId, text);
            }
        }

        public void PostMessage(string channel, string text)
        {
            var found = ulong.TryParse(channel, out var channelId);

            if (found)
            {
                var discordChannel = _discordClient.GetChannelAsync(channelId).Result;
                _discordClient.SendMessageAsync(discordChannel, text);
            }
        }

        public void PostMessage(string text)
        {
            var discordChannel = _discordClient.GetChannelAsync(General).Result;
            _discordClient.SendMessageAsync(discordChannel, text);
        }

        private Task OnConnected(ReadyEventArgs e)
        {
            WriteLine("Connected to the Discord service");
            _isReady = true;
            return Task.CompletedTask;
        }

        private Task OnError(ClientErrorEventArgs e)
        {
            WriteLine($"Exception occurred: {e.Exception.GetType()}: {e.Exception.Message}");
            return Task.CompletedTask;
        }

        private Task OnMessage(MessageCreateEventArgs e)
        {
            if (e.Author != null)
            {
                if (e.Message?.Content != null && e.Message.Content.StartsWith("!"))
                {
                    ProcessCommand(e);
                }

                var user = e?.Author?.Username ?? string.Empty;
                var text = e?.Message?.Content ?? "<< none >>";

                WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");
            }

            return Task.CompletedTask;
        }

        private Task OnMessageEdit(MessageUpdateEventArgs e)
        {
            if (e.Message?.Content != null && e.Message.Content.StartsWith("!"))
            {
                ProcessCommand(e);
            }

            var user = e?.Author?.Username ?? string.Empty;
            var text = e?.Message?.Content ?? "<< deleted >>";

            WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");

            return Task.CompletedTask;
        }

        private void SetupEvents()
        {
            // Wire up basic connectivity events
            _discordClient.Ready += OnConnected;
            _discordClient.ClientErrored += OnError;

            // Wire up additional events
            _discordClient.MessageCreated += OnMessage;
            _discordClient.MessageUpdated += OnMessageEdit;
        }
        private void SetupDiscordCommands()
        {
            var commandConfig = new CommandsNextConfiguration {
                StringPrefix = "~",
                EnableDms = true,
                EnableMentionPrefix = false
            };

            Commands = _discordClient.UseCommandsNext(commandConfig);
            Commands.RegisterCommands<UtilityCommands>(); // Discord only commands
        }

        private void ProcessCommand(DiscordEventArgs e)
        {
            var clientType = GetType().ToString();
            var argsList = new List<string>();
            DiscordUser user;
            string channel, cmdText;

            (user, channel, cmdText) = ExtractFromEventArgs(e);

            if (user.IsBot)
            {
                return;
            }

            var commandPieces = cmdText.Split(' ');
            var command = commandPieces[0].Replace("!", string.Empty);
            var userName = user?.Username ?? string.Empty;
            var userId = user?.Id.ToString() ?? "0"; 

            for (var i = 1; i < commandPieces.Length; i++)
            {
                argsList.Add(commandPieces[i]);
            }

            OnChatCommandReceived?.Invoke(this, new Core.ChatCommandEventArgs(command, argsList,
                channel, userName, userId, clientType, Core.UserRole.Moderator));
        }

        private (DiscordUser user, string channel, string cmdText) ExtractFromEventArgs(DiscordEventArgs e)
        {
            switch (e)
            {
                case MessageUpdateEventArgs u:
                    return (u.Author, u.Channel.Id.ToString(), u.Message.Content.ToLower());
                case MessageCreateEventArgs c:
                    return (c.Author, c.Channel.Id.ToString(), c.Message.Content.ToLower());
            }

            return (null, string.Empty, string.Empty);
        }
    }
}
