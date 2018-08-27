namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class FoodRation : AdventureItem
    {
        internal FoodRation(IReadonlyAdventureGame game, params string[] nouns) : base(game, nouns)
        {
            ItemId = Item.FoodRation;
            Name = "packet of dried *food* rations";
            PluralName = "packets of dried *food* rations";
            IsPortable = true;
            IsEndlessSupply = true;
        }
    }
}

