using Essenbee.Bot.Core.Games.Adventure.Interactions;
using System.Text;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Leaflet : AdventureItem
    {
        internal Leaflet(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Leaflet;
            Name = "*leaflet*";
            PluralName = "*leaflets*";
            IsPortable = true;

            var whenRead = new StringBuilder("You read the leaflet and this is what it says:");
            whenRead.AppendLine();
            whenRead.AppendLine("Somewhere nearby lies the fabled Colossal Cave, a place of danger, mystery and, some say, magic.");
            whenRead.AppendLine("Rumours are that some have found gold in the caves. Other rumours say that no one who goes in ever comes out again...");

            var read = new ItemInteraction(Game, "read");
            read.RegisteredInteractions.Add(new Display(whenRead.ToString()));
            Interactions.Add(read);
        }
    }
}
