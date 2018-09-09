using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTests.AdventureGameTests
{
    [TestClass]
    public class OpenShould
    {
        [TestMethod]
        public void SetItemToOpen_GivenItIsClosedAndNotLocked()
        {
            // Arrange
            var fakePlayer = A.Fake<IAdventurePlayer>();
            var fakeGame = A.Fake<IReadonlyAdventureGame>();

            var fakeCage = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeCage.ItemId).Returns(Item.Cage);
            A.CallTo(() => fakeCage.IsPortable).Returns(true);
            A.CallTo(() => fakeCage.IsMatch("cage")).Returns(true);
            fakeCage.IsOpen = false;

            // Act
            var action = new Open();
            var result = action.Do(fakePlayer, fakeCage);

            // Assert
            Assert.IsTrue(fakeCage.IsOpen);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DoNothing_GivenItemIsLocked()
        {
            // Arrange
            var fakePlayer = A.Fake<IAdventurePlayer>();
            var fakeGame = A.Fake<IReadonlyAdventureGame>();

            var fakeCage = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeCage.ItemId).Returns(Item.Cage);
            A.CallTo(() => fakeCage.IsPortable).Returns(true);
            A.CallTo(() => fakeCage.IsMatch("cage")).Returns(true);
            fakeCage.IsOpen = false;
            fakeCage.IsLocked = true;

            // Act
            var action = new Open();
            var result = action.Do(fakePlayer, fakeCage);

            // Assert
            Assert.IsFalse(fakeCage.IsOpen);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DoNothing_GivenItemIsAlreadyOpen()
        {
            // Arrange
            var fakePlayer = A.Fake<IAdventurePlayer>();
            var fakeGame = A.Fake<IReadonlyAdventureGame>();

            var fakeCage = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeCage.ItemId).Returns(Item.Cage);
            A.CallTo(() => fakeCage.IsPortable).Returns(true);
            A.CallTo(() => fakeCage.IsMatch("cage")).Returns(true);
            fakeCage.IsOpen = true;

            // Act
            var action = new Open();
            var result = action.Do(fakePlayer, fakeCage);

            // Assert
            Assert.IsTrue(fakeCage.IsOpen);
            Assert.IsFalse(result);
        }
    }
}
