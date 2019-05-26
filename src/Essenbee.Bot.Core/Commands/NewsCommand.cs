using System;
using Essenbee.Bot.Core.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace Essenbee.Bot.Core.Commands
{
    class NewsCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName { get => "news"; }
        public string HelpText { get; } = @"The command !news with no argument will display Home news from Sky News. Valid arguments are: " +
                        "space, tech, uk, us, world, business, politics, entertainment, sport or strange.";

        public TimeSpan Cooldown { get; }

        private readonly IBot _bot;

        public NewsCommand(IBot bot)
        {
            _bot = bot;
            Cooldown = TimeSpan.FromMinutes(1);
        }
    
        public Task Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (Status == ItemStatus.Active)
            {
                var text = string.Empty;
                var newsXML = string.Empty;
                var webClient = new WebClient();

                var arg0 = e.ArgsAsList.Count > 0 ? e.ArgsAsList[0] : string.Empty;

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

            return null;
        }

        public bool ShouldExecute()
        {
            return Status == ItemStatus.Active;
        }
    }
}
