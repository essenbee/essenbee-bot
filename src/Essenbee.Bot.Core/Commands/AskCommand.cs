using Essenbee.Bot.Core.Interfaces;
using System.Text;

namespace Essenbee.Bot.Core.Commands
{
    public class AskCommand : ICommand
    {
        private readonly IBot _bot;
        private readonly IAnswerSearchEngine _searchEngine;

        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName => "ask";
        public string HelpText => "The !ask command uses the experimental Project Answer Search to try to answer your questions.";

        public AskCommand(IBot bot)
        {
            _bot = bot;
            _searchEngine = bot.AnswerSearchEngine;
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
            var answerResponse = _searchEngine.GetAnswer(searchFor);
            chatClient.PostMessage(e.Channel, answerResponse);
        }

        public bool ShouldExecute()
        {
            return Status == ItemStatus.Active;
        }
    }
}

