namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public static class ItemFactory
    {
        public static AdventureItem GetInstance(IReadonlyAdventureGame game, string itemType)
        {
            switch(itemType)
            {
                case "bottle":
                    return new Bottle(game);
                case "shard":
                    return new ShardOfGlass(game);
                case "food":
                    return new FoodRation(game);
                case "grate":
                    return new Grate(game);
                case "key":
                    return new Key(game);
                case "lamp":
                    return new Lamp(game);
                case "leaflet":
                    return new Leaflet(game);
                case "mailbox":
                    return new Mailbox(game);
                case "water":
                    return new PintOfWater(game);
                case "glass":
                    return new BrokenGlass(game);
                default:
                    break;
            }

            return null;
        }
    }
}
