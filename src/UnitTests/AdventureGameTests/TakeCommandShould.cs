using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Games.Adventure;
using Essenbee.Bot.Core.Games.Adventure.Commands;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.AdventureGameTests
{
    [TestClass]
    public class TakeCommandShould
    {
        [TestMethod]
        public void AddValidItemToInventory_GivenNotCarried()
        {
            // Arrange
            var inventory = new Inventory();

            var fakeLocation = A.Fake<IAdventureLocation>();
            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            A.CallTo(() => fakePlayer.CurrentLocation).Returns(fakeLocation);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var food = ItemFactory.GetInstance(fakeGame, Item.FoodRation);
            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { food });

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "food" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsTrue(inventory.Has(Item.FoodRation));
            Assert.AreEqual(1, inventory.GetItems().Count(i => i.ItemId == Item.FoodRation));
        }

        [TestMethod]
        public void DoesNotDuplicateItem_GivenAlreadyCarried()
        {
            // Arrange
            var inventory = new Inventory();

            var fakeLocation = A.Fake<IAdventureLocation>();
            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            A.CallTo(() => fakePlayer.CurrentLocation).Returns(fakeLocation);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var food = ItemFactory.GetInstance(fakeGame, Item.FoodRation);
            inventory.AddItem(food);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { food });

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "food" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsTrue(inventory.Has(Item.FoodRation));
            Assert.AreEqual(1, inventory.GetItems().Count(i => i.ItemId == Item.FoodRation));
        }

        [TestMethod]
        public void NotTakeItem_GivenNotPortable()
        {
            // Arrange
            var inventory = new Inventory();

            var fakeLocation = A.Fake<IAdventureLocation>();
            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            A.CallTo(() => fakePlayer.CurrentLocation).Returns(fakeLocation);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var mailbox = ItemFactory.GetInstance(fakeGame, Item.Mailbox);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { mailbox });

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "mailbox" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsFalse(inventory.Has(Item.Mailbox));
        }

        [TestMethod]
        public void NotTakeItem_GivenNotPresent()
        {
            // Arrange
            var inventory = new Inventory();

            var fakeLocation = A.Fake<IAdventureLocation>();
            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            A.CallTo(() => fakePlayer.CurrentLocation).Returns(fakeLocation);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var food = ItemFactory.GetInstance(fakeGame, Item.FoodRation);
            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { food });

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "cage" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsFalse(inventory.Has(Item.Cage));
        }

        [TestMethod]
        public void NotTakeItem_GivenItRequiresContainerNotCarried()
        {
            // Arrange
            var inventory = new Inventory();

            var fakeLocation = A.Fake<IAdventureLocation>();
            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            A.CallTo(() => fakePlayer.CurrentLocation).Returns(fakeLocation);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var bird = ItemFactory.GetInstance(fakeGame, Item.Bird);
            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { bird });

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "bird" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsFalse(inventory.Has(Item.Bird));
        }

        [TestMethod]
        public void AddItemToInventory_GivenItRequiresContainerThatIsCarried()
        {
            // Arrange
            var inventory = new Inventory();

            var fakeLocation = A.Fake<IAdventureLocation>();
            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            A.CallTo(() => fakePlayer.CurrentLocation).Returns(fakeLocation);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var bird = ItemFactory.GetInstance(fakeGame, Item.Bird);
            var cage = ItemFactory.GetInstance(fakeGame, Item.Cage);

            inventory.AddItem(cage);
            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { bird });

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "bird" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsTrue(inventory.HasRequiredContainer(bird));
            Assert.IsTrue(inventory.Has(Item.Cage));
            Assert.IsTrue(inventory.Has(Item.Bird));
            Assert.IsTrue(inventory.GetItems().Single(i => i.IsMatch("cage")).Contents.Contains(bird));
        }

        [TestMethod]
        public void NotTakeItem_GivenItIsPreventedByItemCarried()
        {
            // Example: Cannot take the bord if player is carrying the rod ...

            // Arrange
            var inventory = new Inventory();

            var fakeLocation = A.Fake<IAdventureLocation>();
            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            A.CallTo(() => fakePlayer.CurrentLocation).Returns(fakeLocation);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var bird = ItemFactory.GetInstance(fakeGame, Item.Bird);
            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { bird });

            var rod = ItemFactory.GetInstance(fakeGame, Item.Rod);
            inventory.AddItem(rod);

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "bird" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsFalse(inventory.Has(Item.Bird));
        }

        [TestMethod]
        public void AddNewInstanceOfItemToInventory_GivenEndlessSupply()
        {
            // Arrange
            var inventory = new Inventory();

            var fakeLocation = A.Fake<IAdventureLocation>();
            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            A.CallTo(() => fakePlayer.CurrentLocation).Returns(fakeLocation);

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            var lamp = ItemFactory.GetInstance(fakeGame, Item.Lamp);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { lamp });

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "lamp" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsTrue(inventory.Has(Item.Lamp));
            Assert.AreNotSame(lamp, inventory.GetItems().Single(i => i.ItemId == Item.Lamp));
        }
    }
}
