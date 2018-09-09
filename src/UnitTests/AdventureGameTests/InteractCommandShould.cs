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
        public void DoSomething_GivenItemCarriedInContainer()
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

            var fakeCage = A.Fake<IAdventureItem>();
            A.CallTo(() => fakeCage.ItemId).Returns(Item.Cage);
            A.CallTo(() => fakeCage.Nouns).Returns(new List<string> { "cage" });
            A.CallTo(() => fakeCage.IsPortable).Returns(true);
            A.CallTo(() => fakeCage.IsMatch("cage")).Returns(true);
            A.CallTo(() => fakeCage.IsContainer).Returns(true);
            A.CallTo(() => fakeCage.Contents).Returns(new List<IAdventureItem> { fakeBird });

            var interactions = new List<IInteraction>();
            var actions = new List<IAction>();
            var fakeInteraction = A.Fake<IInteraction>();
            var fakeAction = A.Fake<IAction>();

            A.CallTo(() => fakeAction.Do(A<IAdventurePlayer>.Ignored, fakeBird)).Returns(true);

            actions.Add(fakeAction);
            interactions.Add(fakeInteraction);

            A.CallTo(() => fakeBird.Interactions).Returns(interactions);
            A.CallTo(() => fakeInteraction.ShouldExecute()).Returns(true);
            A.CallTo(() => fakeInteraction.RegisteredInteractions).Returns(actions);
            A.CallTo(() => fakeBird.Interact("free", fakePlayer)).Returns(true);

            inventory.AddItem(fakeCage);

            var args = new ChatCommandEventArgs("!adv", new List<string> { "free", "bird" }, string.Empty, "Bill", "Player1", string.Empty);

            // Act
            var cmd = new Interact(fakeGame, "free");
            cmd.Invoke(fakePlayer, args);

            // Assert
            A.CallTo(() => fakeBird.Interact("free", fakePlayer)).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void DoSomething_GivenItemAtLocation()
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

            A.CallTo(() => fakeLocation.Items).Returns(new List<IAdventureItem> { fakeLamp });

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
