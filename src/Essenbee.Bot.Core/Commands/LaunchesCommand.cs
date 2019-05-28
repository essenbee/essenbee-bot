using Essenbee.Bot.Core.Classes;
using Essenbee.Bot.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Essenbee.Bot.Core.Commands
{
    public class LaunchesCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName => "launches";
        public string HelpText { get; } = @"The command !launches with no argument will display SpaceX launches. Valid arguments are: " +
                "spacex, ula, boeing, lm (or lockheed), nasa, irso (or india), jaxa (or japan), rocketlab (or rl), rfsa (or roscosmos), " +
                "asa (or ariane) and casic (or china).";
        public TimeSpan Cooldown { get; }

        private readonly IBot _bot;

        public LaunchesCommand(IBot bot)
        {
            _bot = bot;
            Cooldown = TimeSpan.FromMinutes(1);
        }

        public bool ShouldExecute() => Status == ItemStatus.Active;

        public Task Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            var client = new HttpClient();
            var provider = "121"; // SpaceX is the default launch provider
            var providerName = "spacex";

            if (e.ArgsAsList.Count > 0)
            {
                providerName = e.ArgsAsList[0];

                switch (e.ArgsAsList[0])
                {
                    case "ula":
                        provider = "124";
                        providerName = "United Launch Alliance";
                        break;
                    case "nasa":
                        provider = "44";
                        providerName = "NASA";
                        break;
                    case "isro":
                    case "india":
                        provider = "31";
                        providerName = "the I.S.R.O (India)";
                        break;
                    case "rl":
                    case "rocketlab":
                        provider = "147";
                        providerName = "Rocket Lab Ltd";
                        break;
                    case "jaxa":
                    case "japan":
                        provider = "37";
                        providerName = "the J.A.X.A (Japan)";
                        break;
                    case "rfsa":
                    case "roscosmos":
                        providerName = "ROSCOSMOS (Russia)";
                        provider = "63";
                        break;
                    case "casic":
                    case "china":
                        provider = "88";
                        providerName = "the C.A.S.I.C. (China)";
                        break;
                    case "spacex":
                        provider = "121";
                        providerName = "SpaceX";
                        break;
                    case "boeing":
                        provider = "80";
                        providerName = "Boeing";
                        break;
                    case "lm":
                    case "lockheed":
                        provider = "82";
                        providerName = "Lockheed-Martin";
                        break;
                    case "asa":
                    case "ariane":
                        provider = "115";
                        providerName = "Ariane Group";
                        break;
                }
            }

            var url = $"https://launchlibrary.net/1.4/launch?lsp={provider}&&mode=list&&startdate={DateTime.Now:yyyy-MM-dd}";
            var endpoint = new Uri(url);
            var result = client.GetAsync(endpoint).Result;
            var launchJson = result.Content.ReadAsStringAsync().Result;
            var rocketLaunch = JsonConvert.DeserializeObject<RocketLaunches>(launchJson);

            if (rocketLaunch.Status != "error")
            {
                var output = new StringBuilder();
                output.AppendLine($"Launch schedule for {providerName}:");

                foreach (var launch in rocketLaunch.Launches)
                {
                    output.AppendLine($"{launch.Name} - {launch.Net}");
                }

                chatClient.PostMessage(e.Channel, output.ToString());
            }
            else
            {
                chatClient.PostMessage(e.Channel, $"No upcoming launches found for {providerName}.");
            }

            return null;
        }
    }
}
