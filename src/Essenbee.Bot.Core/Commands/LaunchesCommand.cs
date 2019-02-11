using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Essenbee.Bot.Core.Classes;
using Essenbee.Bot.Core.Interfaces;
using Newtonsoft.Json;

namespace Essenbee.Bot.Core.Commands
{
    public class LaunchesCommand: ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName { get => "launches"; }
        public string HelpText { get; }
        public TimeSpan Cooldown { get; }

        private readonly IBot _bot;

        public LaunchesCommand(IBot bot)
        {
            _bot = bot;
            Cooldown = TimeSpan.FromMinutes(10);
        }

        public bool ShouldExecute()
        {
            return Status == ItemStatus.Active;
        }

        public void Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            var client = new HttpClient();
            var url = $"https://launchlibrary.net/1.4/launch?lsp=121&&mode=list&&startdate={DateTime.Now:yyyy-MM-dd}";
            var endpoint = new Uri(url);

            var result = client.GetAsync(endpoint).Result;

            var launchJson = result.Content.ReadAsStringAsync().Result;

            var launches = JsonConvert.DeserializeObject<RocketLaunches>(launchJson);
        }

    }
}
