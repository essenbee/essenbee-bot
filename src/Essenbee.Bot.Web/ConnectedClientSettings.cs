namespace Essenbee.Bot.Web
{
    public class ConnectedClientSettings
    {
        public bool EnableConsole { get; set; }
        public bool EnableTwitch { get; set; }
        public bool EnableSlack { get; set; }
        public bool EnableDiscord { get; set; }
        public string SlackDefaultChannel { get; set; }
        public string DiscordDefaultChannel { get; set; }
    }
}
