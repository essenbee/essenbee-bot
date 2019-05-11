using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.Discord;
using Essenbee.Bot.Infra.Slack;
using Essenbee.Bot.Infra.Twitch;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;

namespace Essenbee.Bot.Web
{
    public class ConnectedClients : IConnectedClients
    {
        private IOptions<UserSecrets> _config;
        private readonly IOptions<ConnectedClientSettings> _clientSettings;

        public List<IChatClient> ChatClients { get; }

        public ConnectedClients(IOptions<UserSecrets> config, IOptions<ConnectedClientSettings> clientSettings)
        {
            _config = config;
            _clientSettings = clientSettings;
            ChatClients = ConnectChatClients();
        }

        private List<IChatClient> ConnectChatClients()
        {
            var connectedClients = new List<IChatClient>();

            if (_clientSettings.Value.EnableConsole)
            {
                connectedClients.Add(new ConsoleChatClient());
            }

            if (_clientSettings.Value.EnableTwitch)
            {
                connectedClients.Add(
                    new TwitchChatClient(_config.Value.TwitchUsername, 
                                         _config.Value.TwitchToken, 
                                         _config.Value.TwitchChannel));
            }

            if (_clientSettings.Value.EnableSlack)
            {
                connectedClients.Add(new SlackChatClient(_config.Value.SlackApiKey, 
                    _clientSettings.Value.SlackDefaultChannel));
            }

            if (_clientSettings.Value.EnableDiscord)
            {
                connectedClients.Add(new DiscordChatClient(_config.Value.DiscordToken, 
                    _clientSettings.Value.DiscordDefaultChannel));
            }

            Thread.Sleep(2000);

            return connectedClients;
        }
    }
}
