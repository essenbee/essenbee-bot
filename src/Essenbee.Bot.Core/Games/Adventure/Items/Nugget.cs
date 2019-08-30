using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class Nugget : AdventureItem
    {
        internal Nugget(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.Nugget;
            Name = "large sparkling *nugget* of gold";
            PluralName = "large sparkling nuggets of *gold*";
            IsPortable = true;
            IsTreasure = true;
            GivesPlayerStatus = PlayerStatus.IsEncumbered;
        }
    }
}
