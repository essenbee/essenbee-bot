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
            var fakeFood = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeFood.ItemId).Returns(Item.FoodRation);
            A.CallTo(() => fakeFood.Nouns).Returns(new List<string> { "food" });
            A.CallTo(() => fakeFood.IsPortable).Returns(true);
            A.CallTo(() => fakeFood.IsMatch("food")).Returns(true);
            inventory.AddItem(fakeFood);

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
            var fakeCage = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeCage.ItemId).Returns(Item.Cage);
            A.CallTo(() => fakeCage.Nouns).Returns(new List<string> { "cage" });
            A.CallTo(() => fakeCage.IsPortable).Returns(true);
            A.CallTo(() => fakeCage.IsMatch("cage")).Returns(true);
            A.CallTo(() => fakeCage.IsContainer).Returns(true);
            A.CallTo(() => fakeCage.Contents).Returns(new List<IAdventureItem>());

            var fakeBird = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeBird.ItemId).Returns(Item.Bird);
            A.CallTo(() => fakeBird.Nouns).Returns(new List<string> { "bird" });
            A.CallTo(() => fakeBird.IsPortable).Returns(true);
            A.CallTo(() => fakeBird.IsMatch("bird")).Returns(true);
            A.CallTo(() => fakeBird.ContainerRequired()).Returns(true);
            A.CallTo(() => fakeBird.MustBeContainedIn).Returns(Item.Cage);

            inventory.AddItem(fakeCage);

            fakeCage.Contents.Add(fakeBird);
            inventory.AddItem(fakeCage);

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
            var fakeFood = A.Fake<IAdventureItem>();

            A.CallTo(() => fakeFood.ItemId).Returns(Item.FoodRation);
            A.CallTo(() => fakeFood.Nouns).Returns(new List<string> { "food" });
            A.CallTo(() => fakeFood.IsPortable).Returns(true);
            A.CallTo(() => fakeFood.IsMatch("food")).Returns(true);

            inventory.AddItem(fakeFood);

            var args = new ChatCommandEventArgs("!adv", new List<string> { "drop", "bottle" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Drop(fakeGame, "drop");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsTrue(inventory.Has(Item.FoodRation));
        }
    }
}
