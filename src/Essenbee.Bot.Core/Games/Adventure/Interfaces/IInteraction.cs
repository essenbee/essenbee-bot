using System.Collections.Generic;

namespace Essenbee.Bot.Core.Games.Adventure.Interfaces
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
