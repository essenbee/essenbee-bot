using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests.AdventureGameTests
{
    [TestClass]
    public class ActivateItemShould
    {
        [TestMethod]
        public void SetItemToActive()
        {
            // Arrange
            var fakePlayer = A.Fake<IAdventurePlayer>();
            var fakeGame = A.Fake<IReadonlyAdventureGame>();

            var fakeLamp = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeLamp.ItemId).Returns(Item.Lamp);
            A.CallTo(() => fakeLamp.Nouns).Returns(new List<string> { "lamp" });
            A.CallTo(() => fakeLamp.IsEndlessSupply).Returns(false);
            A.CallTo(() => fakeLamp.IsPortable).Returns(true);
            A.CallTo(() => fakeLamp.IsMatch("lamp")).Returns(true);

            // Act
            var action = new ActivateItem(string.Empty);
            var result = action.Do(fakePlayer, fakeLamp);

            // Assert
            Assert.IsTrue(fakeLamp.IsActive);
            Assert.IsTrue(result);
        }
    }
}
