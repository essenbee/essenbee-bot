﻿using Essenbee.Bot.Core.Interfaces;
using System.Net;

namespace Essenbee.Bot.Core.Commands
{
    class NewsCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName { get => "news"; }
        public string HelpText { get; }

        public NewsCommand()
        {
            HelpText = @"!news with no agrument will display Home news from Sky News. valid arguments are: 
                            space, tech, uk, us, world, business, politics, entertainment, sport or strange";
        }

        public void Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (Status != ItemStatus.Active) return;

            var text = string.Empty;
            var newsXML = string.Empty;
            var webClient = new WebClient();

            var arg0 = e.ArgsAsList[0];

            switch (arg0)
            {
                case "space":
                    newsXML = webClient.DownloadString("http://spaceflightnow.com/feed");
                    break;
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
                case "sport":
                case "sports":
                    newsXML = webClient.DownloadString("http://feeds.bbci.co.uk/sport/rss.xml?edition=uk");
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

            chatClient.PostMessage(e.Channel, text);
        }

        public bool ShoudExecute()
        {
            return Status == ItemStatus.Active;
        }
    }
}
