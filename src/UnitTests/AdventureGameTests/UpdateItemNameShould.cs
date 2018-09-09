using Essenbee.Bot.Core.Games.Adventure.Interactions;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Items;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests.AdventureGameTests
{
    [TestClass]
    public class UpdateItemNameShould
    {
        [TestMethod]
        public void ChangeItemName()
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
            fakeLamp.Name = "lamp";

            // Act
            var action = new UpdateItemName("smashed lamp");
            var result = action.Do(fakePlayer, fakeLamp);

            // Assert
            Assert.AreEqual("smashed lamp", fakeLamp.Name);
            Assert.IsTrue(result);
        }
    }
}
