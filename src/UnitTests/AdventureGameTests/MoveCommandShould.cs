using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Games.Adventure;
using Essenbee.Bot.Core.Games.Adventure.Commands;
using Essenbee.Bot.Core.Games.Adventure.Interfaces;
using Essenbee.Bot.Core.Games.Adventure.Locations;
using Essenbee.Bot.Core.Interfaces;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace UnitTests.AdventureGameTests
{
    [TestClass]
    public class MoveCommandShould
    {
        [TestMethod]
        public void IncrementPlayerMoveCount()
        {
            // Arrange
            var fakeRoad = A.Fake<IAdventureLocation>();
            A.CallTo(() => fakeRoad.ValidMoves).Returns(new List<IPlayerMove> {
                new PlayerMove("You enter the building.", Location.Building, "east", "e", "enter", "in", "inside", "building"),
            });

            var fakeBuilding = A.Fake<IAdventureLocation>();

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            A.CallTo(() => fakeGame.Dungeon.TryGetLocation(A<Location>.Ignored, out fakeBuilding)).Returns(true)
                .AssignsOutAndRefParameters(fakeBuilding);

            var player = new AdventurePlayer("Player1", "PlayerOne", A.Fake<IChatClient>());
            player.CurrentLocation = fakeRoad;

            var args = new ChatCommandEventArgs("!adv", new List<string> { "go", "east" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Move(fakeGame, "move", "go");
            cmd.Invoke(player, args);

            // Assert
            Assert.AreEqual(1, player.Moves);
        }

        [TestMethod]
        public void ChangePlayerLocation_GivenValidMove()
        {
            // Arrange
            var fakeRoad = A.Fake<IAdventureLocation>();
            A.CallTo(() => fakeRoad.ValidMoves).Returns(new List<IPlayerMove> {
                new PlayerMove("You enter the building.", Location.Building, "east", "e", "enter", "in", "inside", "building"),
            });

            var fakeBuilding = A.Fake<IAdventureLocation>();

            var fakeGame = A.Fake<IReadonlyAdventureGame>();
            A.CallTo(() => fakeGame.Dungeon.TryGetLocation(A<Location>.Ignored, out fakeBuilding)).Returns(true)
                .AssignsOutAndRefParameters(fakeBuilding);

            var player = new AdventurePlayer("Player1", "PlayerOne", A.Fake<IChatClient>());
            player.CurrentLocation = fakeRoad;

            var args = new ChatCommandEventArgs("!adv", new List<string> { "go", "east" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Move(fakeGame, "move", "go");
            cmd.Invoke(player, args);

            // Assert
            Assert.AreSame(fakeBuilding, player.CurrentLocation);
            Assert.AreSame(fakeRoad, player.PriorLocation);
        }

        [TestMethod]
        public void NotChangePlayerLocation_GivenInvalidMove()
        {
            // Arrange
            var fakeRoad = A.Fake<IAdventureLocation>();
            A.CallTo(() => fakeRoad.ValidMoves).Returns(new List<IPlayerMove> {
                new PlayerMove("You enter the building.", Location.Building, "east", "e", "enter", "in", "inside", "building"),
            });

            var fakeBuilding = A.Fake<IAdventureLocation>();
            var fakeGame = A.Fake<IReadonlyAdventureGame>();

            var player = new AdventurePlayer("Player1", "PlayerOne", A.Fake<IChatClient>());
            player.CurrentLocation = fakeRoad;

            var args = new ChatCommandEventArgs("!adv", new List<string> { "go", "north" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Move(fakeGame, "move", "go");
            cmd.Invoke(player, args);

            // Assert
            Assert.AreSame(fakeRoad, player.CurrentLocation);
        }
    }
}
