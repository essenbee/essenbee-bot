using System;
using System.Collections.Generic;
using Essenbee.Bot.Core.Interfaces;
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

            // Wire up additional events
            _slackClient.Message += OnMessage;

            _slackClient.Connect();
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
                Console.WriteLine("SleckChatClient is not connected to Slack service!");
            }
        }

        private void OnConnected()
        {
            _isReady = true;
            _connectionFailures = 0;
            Console.WriteLine("Connected to Slack service");
        }

        private void OnHello(HelloEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tHello");
            var channels = _slackClient.Channels.List()?.channels ?? new List<RTM.channel>();

            Console.WriteLine("The following channels exist:");

            foreach (var channel in channels)
            {
                Channels.Add(channel.name, channel.id);
                Console.WriteLine($"\t* {channel.name} ({channel.id})");
            }

            Console.WriteLine();
        }

        private void OnDisconnected()
        {
            if (_shutdown)
            {   
                return;
            }

            try
            {
                _connectionFailures++;
                System.Threading.Thread.Sleep(_connectionFailures < 13 ? 5000 : 60_000);
                Console.WriteLine("Attempting to reconnect to Slack service. Attempt " + _connectionFailures);
                _slackClient.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to handle service disconnect.\r\n" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private static void OnMessage(MessageEventArgs e)
        {
            if (e.user == null)
            {
                return;
            }

            Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tMessage.\t\t[" + e.UserInfo.name + "] [" + e.text + "]");
            // Process_Message(e.UserInfo.name, e.channel, e.text);
        }
    }
}
