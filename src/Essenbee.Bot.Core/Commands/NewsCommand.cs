using Essenbee.Bot.Core.Interfaces;
using System;
using System.Net;

namespace Essenbee.Bot.Core.Commands
{
    class NewsCommand : ICommand

    {
        private IChatClient _chatClient;

        public ItemStatus Status { get; set; }
        public string CommandName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string HelpText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public NewsCommand(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public void Execute(ChatCommandEventArgs e)
        {
            if (!ShoudExecute()) return;

            var text = string.Empty;
            var newsXML = string.Empty;
            var webClient = new WebClient();

            var arg0 = e.ArgsAsList[0];

            switch (arg0)
            {
                case "tech":
                    newsXML = webClient.DownloadString("http://feeds.skynews.com/feeds/rss/technology");
                    break;
                case "uk":
                    newsXML = webClient.DownloadString("http://feeds.skynews.com/feeds/rss/uk");
                    break;
                case "world":
                    newsXML = webClient.DownloadString("http://feeds.skynews.com/feeds/rss/world");
                    break;
                case "us":
                    newsXML = webClient.DownloadString("http://feeds.skynews.com/feeds/rss/us");
                    break;
                case "business":
                    newsXML = webClient.DownloadString("http://feeds.skynews.com/feeds/rss/business");
                    break;
                case "politics":
                    newsXML = webClient.DownloadString("http://feeds.skynews.com/feeds/rss/politics");
                    break;
                case "entertainment":
                    newsXML = webClient.DownloadString("http://feeds.skynews.com/feeds/rss/entertainment");
                    break;
                case "strange":
                    newsXML = webClient.DownloadString("http://feeds.skynews.com/feeds/rss/strange");
                    break;
                default:
                    newsXML = webClient.DownloadString("http://feeds.skynews.com/feeds/rss/home");
                    break;
            }

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(newsXML);

            var itemsRemaining = 10;

            foreach (System.Xml.XmlNode xmlNode in xmlDoc.SelectNodes("rss/channel/item"))
            {
                itemsRemaining--;
                if (itemsRemaining == 0)
                {
                    break;
                }
                text +=
                    xmlNode.SelectSingleNode("title").InnerText + "\r\n\t" +
                    xmlNode.SelectSingleNode("link").InnerText + "\r\n";
            }

            _chatClient.PostMessage(e.Channel, text);
        }

        public bool ShoudExecute()
        {
            return Status == ItemStatus.Active;
        }
    }
}
