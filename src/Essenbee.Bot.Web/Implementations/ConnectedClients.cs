using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.Slack;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var connectedClients = new List<IChatClient>
            {
                //new ConsoleChatClient(),
                new SlackChatClient(slackApiKey),
            };

            Thread.Sleep(2000);

            return connectedClients;
        }
    }
}
