
using System;
using Essenbee.Bot.Core.Interfaces;
using Slack;

namespace Essenbee.Bot.Infra.Slack
{
    public class SlackChatClient: IChatClient
    {
        private readonly Client _slackClient;
        private  int _connectionFailures = 0;
        private  bool _shutdown = false;

        public SlackChatClient(string apiKey)
        {
            _slackClient = new Client(apiKey);

            _slackClient.ServiceConnected += ClientConnected;
            _slackClient.ServiceConnectionFailed += new Client.ServiceConnectionFailedEventHandler(DisconnectedConnectionFailure);
            _slackClient.ServiceDisconnected += new Client.ServiceDisconnectedEventHandler(DisconnectedConnectionFailure);

            _slackClient.Connect();
        }

        public void PostMessage(string theChannel, string msg)
        {
            var msgArgs = new Chat.PostMessageArguments
            {
                channel = theChannel,
                text = msg
            };

            _slackClient.Chat.PostMessage(msgArgs);
        }

        private void ClientConnected()
        {
            _connectionFailures = 0;
            Console.WriteLine("Connected to Slack service");
        }

        private void DisconnectedConnectionFailure()
        {
            if (_shutdown)
            {   
                return;
            }

            try
            {
                _connectionFailures++;

                System.Threading.Thread.Sleep(_connectionFailures < 13 ? 5000 : 60_000);

                Console.WriteLine("Attempting to reconnect to slack service. Attempt " + _connectionFailures);

                _slackClient.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not handle service disconnected.\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }
}
