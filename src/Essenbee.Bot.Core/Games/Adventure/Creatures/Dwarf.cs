using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Creatures
{
    public class Dwarf : AdventureCreature
    {
        internal Dwarf(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Dwarf;
            Name = "threatening little *dwarf*";
            PluralName = "threatening little *dwarves*";
            IsPortable = false;
            IsEndlessSupply = false;
            IsTreasure = false;
        }
    }
}
