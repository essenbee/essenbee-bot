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

            var fakeLamp = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeLamp.ItemId).Returns(Item.Lamp);
            A.CallTo(() => fakeLamp.Nouns).Returns(new List<string> { "lamp" });
            A.CallTo(() => fakeLamp.IsEndlessSupply).Returns(false);
            A.CallTo(() => fakeLamp.IsPortable).Returns(true);
            A.CallTo(() => fakeLamp.IsMatch("lamp")).Returns(true);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { fakeLamp });

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "lamp" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsTrue(inventory.Has(Item.Lamp));
            Assert.AreEqual(1, inventory.GetItems().Count(i => i.ItemId == Item.Lamp));
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

            var fakeFood = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeFood.ItemId).Returns(Item.FoodRation);
            A.CallTo(() => fakeFood.Nouns).Returns(new List<string> { "food" });
            A.CallTo(() => fakeFood.IsPortable).Returns(true);
            A.CallTo(() => fakeFood.IsMatch("food")).Returns(true);
            inventory.AddItem(fakeFood);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { fakeFood });

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
            var fakMailbox = A.Fake<IAdventureItem>();
            A.CallTo(() => fakMailbox.ItemId).Returns(Item.Mailbox);
            A.CallTo(() => fakMailbox.Nouns).Returns(new List<string> { "mailbox" });
            A.CallTo(() => fakMailbox.IsPortable).Returns(false);
            A.CallTo(() => fakMailbox.IsMatch("mailbox")).Returns(true);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { fakMailbox });

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
            var fakeFood = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeFood.ItemId).Returns(Item.FoodRation);
            A.CallTo(() => fakeFood.Nouns).Returns(new List<string> { "food" });
            A.CallTo(() => fakeFood.IsPortable).Returns(true);
            A.CallTo(() => fakeFood.IsMatch("food")).Returns(true);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { fakeFood });

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

            var fakeBird = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeBird.ItemId).Returns(Item.Bird);
            A.CallTo(() => fakeBird.Nouns).Returns(new List<string> { "bird" });
            A.CallTo(() => fakeBird.IsPortable).Returns(true);
            A.CallTo(() => fakeBird.IsMatch("bird")).Returns(true);
            A.CallTo(() => fakeBird.ContainerRequired()).Returns(true);
            A.CallTo(() => fakeBird.MustBeContainedIn).Returns(Item.Cage);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { fakeBird });

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

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { fakeBird });
            inventory.AddItem(fakeCage);

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "bird" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsTrue(inventory.HasRequiredContainer(fakeBird));
            Assert.IsTrue(inventory.Has(Item.Cage));
            Assert.IsTrue(inventory.Has(Item.Bird));
            Assert.IsTrue(inventory.GetItems().Single(i => i.IsMatch("cage")).Contents.Contains(fakeBird));
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
            var fakeBird = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeBird.ItemId).Returns(Item.Bird);
            A.CallTo(() => fakeBird.Nouns).Returns(new List<string> { "bird" });
            A.CallTo(() => fakeBird.IsPortable).Returns(true);
            A.CallTo(() => fakeBird.IsMatch("bird")).Returns(true);
            A.CallTo(() => fakeBird.ContainerRequired()).Returns(true);
            A.CallTo(() => fakeBird.MustBeContainedIn).Returns(Item.Cage);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { fakeBird });

            var fakeRod = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeRod.ItemId).Returns(Item.Rod);
            A.CallTo(() => fakeRod.Nouns).Returns(new List<string> { "rod" });
            A.CallTo(() => fakeRod.IsPortable).Returns(true);
            A.CallTo(() => fakeRod.IsMatch("rod")).Returns(true);

            inventory.AddItem(fakeRod);

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

            var fakeLamp = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeLamp.ItemId).Returns(Item.Lamp);
            A.CallTo(() => fakeLamp.Nouns).Returns(new List<string> { "lamp" });
            A.CallTo(() => fakeLamp.IsEndlessSupply).Returns(true);
            A.CallTo(() => fakeLamp.IsPortable).Returns(true);
            A.CallTo(() => fakeLamp.IsMatch("lamp")).Returns(true);

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { fakeLamp });

            var args = new ChatCommandEventArgs("!adv", new List<string> { "take", "lamp" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Take(fakeGame, "take", "get");
            cmd.Invoke(fakePlayer, args);

            // Assert
            Assert.IsTrue(inventory.Has(Item.Lamp));
            Assert.AreNotSame(fakeLamp, inventory.GetItems().Single(i => i.ItemId == Item.Lamp));
        }
    }
}
