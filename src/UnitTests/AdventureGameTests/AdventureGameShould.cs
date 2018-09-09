using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Games.Adventure;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using Essenbee.Bot.Core.Interfaces;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests.AdventureGameTests
{
    [TestClass]
    public class AdventureGameShould
    {
        [TestMethod]
        public void StartWithNoPlayers()
        {
            // Arrange
            var fakeDungeon = A.Fake<IDungeon>();

            // Act
            var sut = new AdventureGame(fakeDungeon);

            // Assert
            Assert.AreEqual(0, sut.Players.Count);
        }

        [TestMethod]
        public void StartWithDungeon()
        {
            // Arrange
            var fakeDungeon = A.Fake<IDungeon>();

            // Act
            var sut = new AdventureGame(fakeDungeon);

            // Assert
            Assert.IsNotNull(sut.Dungeon);
        }

        [TestMethod]
        public void AddPlayer_GivenNotAlredayPlayer()
        {
            // Arrange
            var fakeRoad = A.Fake<IAdventureLocation>();
            var fakeDungeon = A.Fake<IDungeon>();
            A.CallTo(() => fakeDungeon.GetStartingLocation()).Returns(fakeRoad);

            var fakeChatClient = A.Fake<IChatClient>();
            var eventArgs = new ChatCommandEventArgs("!adv", new List<string>(), string.Empty, "PlayerOne", "Player1", string.Empty );
            var sut = new AdventureGame(fakeDungeon);

            // Act
            sut.HandleCommand(fakeChatClient, eventArgs);

            // Assert
            Assert.AreEqual(1, sut.Players.Count);
        }

        [TestMethod]
        public void NotDuplicatePlayers()
        {
            // Arrange
            var fakeRoad = A.Fake<IAdventureLocation>();
            var fakeDungeon = A.Fake<IDungeon>();
            A.CallTo(() => fakeDungeon.GetStartingLocation()).Returns(fakeRoad);

            var fakeChatClient = A.Fake<IChatClient>();
            var eventArgs = new ChatCommandEventArgs("!adv", new List<string>(), string.Empty, "PlayerOne", "Player1", string.Empty);
            var sut = new AdventureGame(fakeDungeon);

            // Act
            sut.HandleCommand(fakeChatClient, eventArgs);
            sut.HandleCommand(fakeChatClient, eventArgs);

            // Assert
            Assert.AreEqual(1, sut.Players.Count);
        }

        [TestMethod]
        public void AllowsMultiplePlayers()
        {
            // Arrange
            var fakeRoad = A.Fake<IAdventureLocation>();
            var fakeDungeon = A.Fake<IDungeon>();
            A.CallTo(() => fakeDungeon.GetStartingLocation()).Returns(fakeRoad);

            var fakeChatClient = A.Fake<IChatClient>();
            var eventArgs1 = new ChatCommandEventArgs("!adv", new List<string>(), string.Empty, "Bill", "Player1", string.Empty);
            var eventArgs2 = new ChatCommandEventArgs("!adv", new List<string>(), string.Empty, "Bob", "Player2", string.Empty);
            var eventArgs3 = new ChatCommandEventArgs("!adv", new List<string>(), string.Empty, "Bill", "Player3", string.Empty);
            var sut = new AdventureGame(fakeDungeon);

            // Act
            sut.HandleCommand(fakeChatClient, eventArgs1);
            sut.HandleCommand(fakeChatClient, eventArgs2);
            sut.HandleCommand(fakeChatClient, eventArgs3);

            // Assert
            Assert.AreEqual(3, sut.Players.Count);
        }

        private static Dictionary<Location, IAdventureLocation> GetSimpleDungeon()
        {
            var fakeRoad = A.Fake<IAdventureLocation>();
            var fakeBuilding = A.Fake<IAdventureLocation>();
            var twoRoomDungeon = new Dictionary<Location, IAdventureLocation>
            {
                { Location.Road, fakeRoad },
                { Location.Building, fakeBuilding },
            };
            return twoRoomDungeon;
        }
    }
}
