using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class RottingDeadDragon : AdventureItem
    {

        internal RottingDeadDragon(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.RottingDeadDragon;
            Name = "The rotting carcass of a dead dragon is lying off to one side.";
            PluralName = "The rotting carcass of a dead dragon is lying off to one side.";
            IsPortable = false;
        }
    }
}
