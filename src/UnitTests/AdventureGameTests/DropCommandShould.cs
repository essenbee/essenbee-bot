using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Games.Adventure;
using Essenbee.Bot.Core.Games.Adventure.Commands;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace UnitTests.AdventureGameTests
{
    [TestClass]
    public class DropCommandShould
    {
        [TestMethod]
        public void RemoveItemFromInventory_GivenCarried()
        {
            // Arrange
            var inventory = new Inventory();

            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var food = ItemFactory.GetInstance(fakeGame, Item.FoodRation);
            inventory.AddItem(food);

            var args = new ChatCommandEventArgs("!adv", new List<string> { "drop", "food" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Drop(fakeGame, "drop");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsFalse(inventory.Has(Item.FoodRation));
        }

        [TestMethod]
        public void RemoveItemFromInventory_GivenContainedItem()
        {
            // Arrange
            var inventory = new Inventory();

            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var cage = ItemFactory.GetInstance(fakeGame, Item.Cage);
            var bird = ItemFactory.GetInstance(fakeGame, Item.Bird);

            cage.Contents.Add(bird);
            inventory.AddItem(cage);

            var args = new ChatCommandEventArgs("!adv", new List<string> { "drop", "bird" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Drop(fakeGame, "drop");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsFalse(inventory.Has(Item.Bird));
            Assert.IsTrue(inventory.Has(Item.Cage));
        }

        [TestMethod]
        public void DoesNothing_GivenItemNotCarried()
        {
            // Arrange
            var inventory = new Inventory();

            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var food = ItemFactory.GetInstance(fakeGame, Item.FoodRation);
            inventory.AddItem(food);

            var args = new ChatCommandEventArgs("!adv", new List<string> { "drop", "bottle" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Drop(fakeGame, "drop");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsTrue(inventory.Has(Item.FoodRation));
        }
    }
}
