using Essenbee.Bot.Core.Games.Adventure.Interfaces;

namespace Essenbee.Bot.Core.Games.Adventure.Items
{
    public static class ItemFactory
    {
        public static IAdventureItem GetInstance(IReadonlyAdventureGame game, Item itemType)
        {
            switch(itemType)
            {
                case Item.Water:
                    return new Water(game, "water");
                case Item.Bottle:
                    return new Bottle(game, "bottle");
                case Item.ShardOfGlass:
                    return new ShardOfGlass(game, "glass", "shard");
                case Item.FoodRation:
                    return new FoodRation(game, "food", "ration");
                case Item.Grate:
                    return Grate.GetInstance(game, "grate"); // Singleton
                case Item.Key:
                    return new Key(game, "key");
                case Item.Lamp:
                    return new Lamp(game, "lamp", "lantern", "headlamp");
                case Item.Leaflet:
                    return new Leaflet(game, "leaflet", "flyer");
                case Item.Mailbox:
                    return new Mailbox(game, "mailbox");
                case Item.PintOfWater:
                    return new PintOfWater(game, "water");
                case Item.BrokenGlass:
                    return new BrokenGlass(game, "glass");
                case Item.Cage:
                    return new Cage(game, "cage");
                case Item.Rod:
                    return new Rod(game, "rod");
                case Item.Bird:
                    return new Bird(game, "bird");
                case Item.Snake:
                    return new Snake(game, "snake", "serpent");
                case Item.DeadSnake:
                    return new DeadSnake(game, "snake", "serpent");
                case Item.CrystalBridge:
                    return CrystalBridge.GetInstance(game, "bridge"); // Singleton
                case Item.Diamond:
                    return new Diamond(game, "diamond", "gem");
                case Item.Nugget:
                    return new Nugget(game, "nugget", "gold");
                case Item.PirateChest:
                    return new PirateChest(game, "chest");
                case Item.BarsOfSilver:
                    return new BarsOfSilver(game, "silver", "bars");
                case Item.Jewelry:
                    return new Jewelry(game, "jewelry", "jewels", "jools", "gems", "jewellery", "jewellry");
                case Item.Coins:
                    return new Coins(game, "coins", "money", "cash");
                case Item.Dragon:
                    return new Dragon(game, "dragon", "wyrm");
                case Item.DeadDragon:
                    return new DeadDragon(game, "dragon", "wyrm");
                case Item.RottingDeadDragon:
                    return new RottingDeadDragon(game, "dragon", "wyrm");
                case Item.DragonTooth:
                    return new DragonTooth(game, "tooth", "teeth");
                case Item.ShadowyFigure:
                    return new ShadowyFigure(game, "figure", "shadow", "shadowy");
            }

            return null;
        }
    }
}
