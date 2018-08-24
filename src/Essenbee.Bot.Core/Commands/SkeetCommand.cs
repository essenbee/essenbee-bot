using Essenbee.Bot.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Commands
{
    class SkeetCommand : ICommand
    {
        public ItemStatus Status { get; set; } = ItemStatus.Draft;
        public string CommandName => "skeet";
        public string HelpText => "Use !skeet to get a random fact about Jon Skeet.";

        private readonly Random _random = new Random();
        private readonly List<string> _quotes;
        private readonly IBot _bot;

        public SkeetCommand(IBot bot)
        {
            _quotes = SkeetQuotes.Quotes;
            _bot = bot;
        }

        public SkeetCommand(List<string> quotes)
        {
            _quotes = quotes;
        }

        public void Execute(IChatClient chatClient, ChatCommandEventArgs e)
        {
            if (Status != ItemStatus.Active) return;

            if (_quotes == null) return;

            chatClient.PostMessage(e.Channel, _quotes[_random.Next(_quotes.Count)]);
        }

        public bool ShouldExecute()
        {
            return Status == ItemStatus.Active;
        }
    }
}
