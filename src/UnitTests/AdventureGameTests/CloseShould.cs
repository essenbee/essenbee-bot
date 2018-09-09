using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.AdventureGameTests
{
    [TestClass]
    public class CloseShould
    {
        [TestMethod]
        public void SetItemToClosed_GivenItIsOpen()
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
            var action = new Close();
            var result = action.Do(fakePlayer, fakeCage);

            // Assert
            Assert.IsFalse(fakeCage.IsOpen);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DoNothing_GivenItemIsClosed()
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
            var action = new Close();
            var result = action.Do(fakePlayer, fakeCage);

            // Assert
            Assert.IsFalse(fakeCage.IsOpen);
            Assert.IsFalse(result);
        }
    }
}
