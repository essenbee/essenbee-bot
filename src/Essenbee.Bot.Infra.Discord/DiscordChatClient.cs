using DSharpPlus;
using DSharpPlus.CommandsNext;
using static System.Console;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Interfaces;
using System;
using System.Collections.Generic;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;
using Essenbee.Bot.Infra.Discord.Commands;
using System.Text;

namespace Essenbee.Bot.Infra.Discord
{
    public class DiscordChatClient : IChatClient
    {
        private readonly DiscordClient _discordClient;
        private bool _isReady;
        private DiscordConfig _settings;
        private const ulong General = 546412212038795307;

        public bool UseUsernameForIM { get; }
        public string DefaultChannel => "general";
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

            SetupEvents();
            SetupCommands();
            _ = Connect();
        }

        public DiscordChatClient(DiscordConfig settings)
        {
            _settings = settings;
            _discordClient = new DiscordClient(new DiscordConfiguration 
            {
                Token = _settings.DiscordToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            });

            SetupEvents();
            SetupCommands();
            _ = Connect();
        }

        public async Task Connect()
        {
            await _discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void PostDirectMessage(string username, string text)
        {
            throw new NotImplementedException();
        }

        public void PostDirectMessage(IAdventurePlayer player, string text)
        {
            throw new NotImplementedException();
        }

        public void PostMessage(string channel, string text)
        {
            var discordChannel = _discordClient.GetChannelAsync(General).Result;
            _discordClient.SendMessageAsync(discordChannel, text);
        }

        public void PostMessage(string text)
        {
            var discordChannel = _discordClient.GetChannelAsync(General).Result;
            _discordClient.SendMessageAsync(discordChannel, text);
        }

        private Task OnConnected(ReadyEventArgs e)
        {
            _isReady = true;
            WriteLine("Connected to the Discord service");

            return Task.CompletedTask;
        }

        private Task OnError(ClientErrorEventArgs e)
        {
            _isReady = true;
            WriteLine($"Exception occurred: {e.Exception.GetType()}: {e.Exception.Message}");

            return Task.CompletedTask;
        }

        private Task OnMessage(MessageCreateEventArgs e)
        {
            if (e.Author == null)
            {
                return Task.CompletedTask;
            }

            if (e.Message.Content.StartsWith("!") )
            {
                var cmdText = e.Message.Content.ToLower();
                var commandPieces = cmdText.Split(' ');
                var command = commandPieces[0].Replace("!", string.Empty);
                var clientType = GetType().ToString();
                var argsList = new List<string>();

                for (var i = 1; i < commandPieces.Length; i++)
                {
                    argsList.Add(commandPieces[i]);
                }


                OnChatCommandReceived?.Invoke(this, new Core.ChatCommandEventArgs(command, argsList, 
                    "general", e.Author.Username, e.Author.Username, clientType, Core.UserRole.Streamer));
            }

            var user = e?.Author?.Username ?? string.Empty;
            var text = e?.Message?.Content ?? "<< none >>";

            WriteLine($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}\tMessage.\t\t[{user}] [{text}]");

            return Task.CompletedTask;
        }

        private Task OnMessageEdit(MessageUpdateEventArgs e)
        {
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
            // _discordClient.CommandReceived += ProcessCommand;
        }
        private void SetupCommands()
        {
            var commandConfig = new CommandsNextConfiguration {
                StringPrefix = "~",
                EnableDms = true,
                EnableMentionPrefix = false
            };

            Commands = _discordClient.UseCommandsNext(commandConfig);
            Commands.RegisterCommands<UtilityCommands>(); // Discord only commands
        }
    }
}
