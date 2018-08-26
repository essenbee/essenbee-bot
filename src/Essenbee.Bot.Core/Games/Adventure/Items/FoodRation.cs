namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public class FoodRation : AdventureItem
    {
        public FoodRation(IReadonlyAdventureGame game) : base(game)
        {
            ItemId = "food";
            Name = "packet of dried *food* rations";
            PluralName = "packets of dried *food* rations";
            IsPortable = true;
            IsEndlessSupply = false; // ToDo: how to handle endless items
        }
    }
}

