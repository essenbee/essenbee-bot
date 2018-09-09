using Essenbee.Bot.Core.Games.Adventure;
using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests.AdventureGameTests
{
    [TestClass]
    public class UnlockShould
    {
        [TestMethod]
        public void SetItemToUnlocked_GivenItIsLockedAndPlayerHasKey()
        {
            // Arrange
            var inventory = new Inventory();

            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            var fakeGame = A.Fake<IReadonlyAdventureGame>();

            var fakeKey = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeKey.ItemId).Returns(Item.Key);
            A.CallTo(() => fakeKey.Nouns).Returns(new List<string> { "key" });
            A.CallTo(() => fakeKey.IsPortable).Returns(true);
            A.CallTo(() => fakeKey.IsMatch("key")).Returns(true);
            inventory.AddItem(fakeKey);

            var fakeCage = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeCage.ItemId).Returns(Item.Cage);
            A.CallTo(() => fakeCage.IsPortable).Returns(true);
            A.CallTo(() => fakeCage.IsMatch("cage")).Returns(true);
            fakeCage.IsOpen = false;
            fakeCage.IsLocked = true;
            fakeCage.ItemIdToUnlock = Item.Key;

            // Act
            var action = new Unlock();
            var result = action.Do(fakePlayer, fakeCage);

            // Assert
            Assert.IsFalse(fakeCage.IsLocked);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DoNothing_GivenItIsUnklocked()
        {
            // Arrange
            var inventory = new Inventory();

            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            var fakeGame = A.Fake<IReadonlyAdventureGame>();

            var fakeKey = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeKey.ItemId).Returns(Item.Key);
            A.CallTo(() => fakeKey.Nouns).Returns(new List<string> { "key" });
            A.CallTo(() => fakeKey.IsPortable).Returns(true);
            A.CallTo(() => fakeKey.IsMatch("key")).Returns(true);
            inventory.AddItem(fakeKey);

            var fakeCage = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeCage.ItemId).Returns(Item.Cage);
            A.CallTo(() => fakeCage.IsPortable).Returns(true);
            A.CallTo(() => fakeCage.IsMatch("cage")).Returns(true);
            fakeCage.IsOpen = false;
            fakeCage.IsLocked = false;
            fakeCage.ItemIdToUnlock = Item.Key;

            // Act
            var action = new Unlock();
            var result = action.Do(fakePlayer, fakeCage);

            // Assert
            Assert.IsFalse(fakeCage.IsLocked);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void DoNothing_GivenPlayerHasNoKey()
        {
            // Arrange
            var inventory = new Inventory();

            var fakePlayer = A.Fake<IAdventurePlayer>();
            A.CallTo(() => fakePlayer.Inventory).Returns(inventory);
            var fakeGame = A.Fake<IReadonlyAdventureGame>();

            var fakeCage = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeCage.ItemId).Returns(Item.Cage);
            A.CallTo(() => fakeCage.IsPortable).Returns(true);
            A.CallTo(() => fakeCage.IsMatch("cage")).Returns(true);
            fakeCage.IsOpen = false;
            fakeCage.IsLocked = true;
            fakeCage.ItemIdToUnlock = Item.Key;

            // Act
            var action = new Unlock();
            var result = action.Do(fakePlayer, fakeCage);

            // Assert
            Assert.IsTrue(fakeCage.IsLocked);
            Assert.IsFalse(result);
        }
    }
}
