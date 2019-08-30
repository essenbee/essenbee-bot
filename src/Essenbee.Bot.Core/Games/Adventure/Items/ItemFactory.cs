using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using System.Collections.Generic;
using System.Linq;

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
                    return new Coins(game, "coins", "coin", "money", "cash");
                case Item.Rug:
                    return new Rug(game, "rug", "carpet", "persian");
                case Item.Dragon:
                    return new Dragon(game, "dragon", "wyrm");
                case Item.DeadDragon:
                    return DeadDragon.GetInstance(game, "dragon", "wyrm"); // Singleton
                case Item.RottingDeadDragon:
                    return new RottingDeadDragon(game, "dragon", "wyrm"); // Singleton
                case Item.DragonTooth:
                    return new DragonTooth(game, "tooth", "teeth");
                case Item.ShadowyFigure:
                    return new ShadowyFigure(game, "figure", "shadow", "shadowy");
                case Item.Magazines:
                    return new Magazines(game, "magazine", "magazines");
                case Item.Pillow:
                    return new Pillow(game, "pillow", "cushion");
                case Item.Vase:
                    return new Vase(game, "vase", "ming");
                case Item.BrokenVase:
                    return new BrokenGlass(game, "pottery");
                case Item.LittleAxe:
                    return new LittleAxe(game, "axe", "hatchet");
                case Item.Dwarf:
                    return new Dwarf(game, "dwarf");
                case Item.Batteries:
                    return new Batteries(game, "batteries");
                case Item.SpentBatteries:
                    return new SpentBatteries(game, "batteries");
                case Item.VendingMachine:
                    return new VendingMachine(game, "vending", "machine", "dispenser");
                case Item.Emerald:
                    return new Emerald(game, "emerald", "jewel");
                case Item.PlatinumPyramid:
                    return new PlatinumPyramid(game, "pyramid");
                case Item.Troll:
                    return new Troll(game, "troll");
            }

            return null;
        }
    }
}
