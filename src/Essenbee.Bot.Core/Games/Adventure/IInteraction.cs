using Essenbee.Bot.Core.Games.Adventure.Interactions;
using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure
{
    public interface IInteraction
    {
        List<string> Verbs { get; }
        IReadonlyAdventureGame Game { get; }
        IList<IAction> RegisteredInteractions { get; set; }

        bool IsMatch(string verb);
        bool ShouldExecute();
    }
}
