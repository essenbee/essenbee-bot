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
    public class InteractCommandShould
    {
        [TestMethod]
        public void DoSomething_GivenItemCarried()
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

            var interactions = new List<IInteraction>();
            var actions = new List<IAction>();
            var fakeInteraction = A.Fake<IInteraction>();         
            var fakeAction = A.Fake<IAction>();

            A.CallTo(() => fakeAction.Do(A<IAdventurePlayer>.Ignored, fakeLamp)).Returns(true);

            actions.Add(fakeAction);
            interactions.Add(fakeInteraction);

            A.CallTo(() => fakeLamp.Interactions).Returns(interactions);
            A.CallTo(() => fakeInteraction.ShouldExecute()).Returns(true);
            A.CallTo(() => fakeInteraction.RegisteredInteractions).Returns(actions);
            A.CallTo(() => fakeLamp.Interact("light", fakePlayer)).Returns(true);

            inventory.AddItem(fakeLamp);

            var args = new ChatCommandEventArgs("!adv", new List<string> { "light", "lamp" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Interact(fakeGame, "light");
            cmd.Invoke(fakePlayer, args);

            // Assert
            A.CallTo(() => fakeLamp.Interact("light", fakePlayer)).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void DoNothing_GivenItemNotCarried()
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

            var interactions = new List<IInteraction>();
            var actions = new List<IAction>();
            var fakeInteraction = A.Fake<IInteraction>();
            var fakeAction = A.Fake<IAction>();

            A.CallTo(() => fakeAction.Do(A<IAdventurePlayer>.Ignored, fakeLamp)).Returns(true);

            actions.Add(fakeAction);
            interactions.Add(fakeInteraction);

            A.CallTo(() => fakeLamp.Interactions).Returns(interactions);
            A.CallTo(() => fakeInteraction.ShouldExecute()).Returns(true);
            A.CallTo(() => fakeInteraction.RegisteredInteractions).Returns(actions);
            A.CallTo(() => fakeLamp.Interact("light", fakePlayer)).Returns(true);

            var args = new ChatCommandEventArgs("!adv", new List<string> { "light", "lamp" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Interact(fakeGame, "light");
            cmd.Invoke(fakePlayer, args);

            // Assert
            A.CallTo(() => fakeLamp.Interact("light", fakePlayer)).MustNotHaveHappened();
        }
    }
}
