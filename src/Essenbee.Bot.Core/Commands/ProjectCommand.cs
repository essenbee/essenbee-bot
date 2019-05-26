using Essenbee.Bot.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Essenbee.Bot.Core.Commands
{
    public class ProjectCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;

        public string CommandName => "project";

        public string HelpText => "Use the !project command to find out what we are working on this stream.";

        public TimeSpan Cooldown => TimeSpan.FromSeconds(10);

        private readonly IBot _bot;

        public ProjectCommand(IBot bot)
        {
            _bot = bot;
        }

        public async Task Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            var projectTextModel = await _bot.BotDataClient.GetProjectText();
            var projectText = projectTextModel.Text;

            if (!string.IsNullOrWhiteSpace(projectText))
            {
                chatClient.PostMessage(e.Channel, projectText);
            }
            else
            {
                chatClient.PostMessage(e.Channel, "Whoops! Looks like essenbee forgot to set the Project text!");
            }
        }

        public bool ShouldExecute()
        {
            return Status == ItemStatus.Active;
        }
    }
}
