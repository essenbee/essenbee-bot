using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.Slack;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Essenbee.Bot.Infra.Twitch;
using Essenbee.Bot.Infra.Discord;

namespace Essenbee.Bot.Web
{
    public class ConnectedClients : IConnectedClients
    {
        private IOptions<UserSecrets> _config;
        public List<IChatClient> ChatClients { get; }

        public ConnectedClients(IOptions<UserSecrets> config)
        {
            _config = config;
            ChatClients = ConnectChatClients();
        }

        private List<IChatClient> ConnectChatClients()
        {
            var slackApiKey = _config.Value.SlackApiKey;
            var discordToken = _config.Value.DiscordToken;
            var connectedClients = new List<IChatClient>
            {
                // new ConsoleChatClient(),
                new SlackChatClient(slackApiKey),
                new DiscordChatClient(discordToken),
                // new TwitchChatClient(_config.Value.TwitchUsername, _config.Value.TwitchToken, _config.Value.TwitchChannel),
            };

            Thread.Sleep(2000);

            return connectedClients;
        }
    }
}
