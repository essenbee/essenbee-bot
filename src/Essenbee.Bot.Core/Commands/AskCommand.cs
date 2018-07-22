using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Infra.CognitiveServices;
using System.Text;

namespace Essenbee.Bot.Core.Commands
{
    public class AskCommand : ICommand
    {
        private readonly IBotSettings _settings;

        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName => "ask";
        public string HelpText => "The !ask command uses the experimental Project Answer Search to try to answer your questions.";

        public AskCommand(IBotSettings settings)
        {
            _settings = settings;
        }

        public void Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (Status != ItemStatus.Active) return;

            if (e.ArgsAsList.Count == 0)
            {
                chatClient.PostMessage(e.Channel, HelpText);
            }

            var searchTerm = new StringBuilder();
            foreach (var arg in e.ArgsAsList)
            {
                searchTerm.Append(arg);
                searchTerm.Append(" ");
            }

            var searchFor = searchTerm.ToString().Trim();
            var searchEngine = new AnswerSearch(_settings.AnswerSerachApiKey);
            var answerResponse = searchEngine.GetAnswer(searchFor);
            chatClient.PostMessage(e.Channel, answerResponse);
        }

        public bool ShoudExecute()
        {
            return Status == ItemStatus.Active;
        }
    }
}

