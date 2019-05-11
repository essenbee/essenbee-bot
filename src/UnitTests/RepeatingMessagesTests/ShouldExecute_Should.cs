using System;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.TimerTriggeredMessageTests
{
    [TestClass]
    public class ShouldExecute_Should
    {
        private const int Delay = 1;

        [TestMethod]
        public void ReturnFalse_WhenCreated()
        {
            //Arrange
            var fakeClock = A.Fake<IClock>();

            A.CallTo(() => fakeClock.UtcNow).Returns(new DateTime(2018, 6, 28, 12, 0, 0));

            var testMsg = new RepeatingMessage("channel","Hi, this is a timed message from AlphaBot!", Delay, null, fakeClock, "TestMessage");
            
            // Act and Assert
            Assert.IsFalse(testMsg.ShouldExecute());
        }

        [TestMethod]
        public void ReturnFalse_AfterHalfDelayExpired()
        {
            //Arrange
            var fakeClock = A.Fake<IClock>();

            var startTime = new DateTime(2018, 6, 28, 12, 0, 0);

            A.CallTo(() => fakeClock.UtcNow).Returns(startTime).Once()
                .Then.Returns(startTime.AddSeconds(30)).Once();

            var testMsg = new RepeatingMessage("channel", "Hi, this is a timed message from AlphaBot!", Delay, null, fakeClock, "TestMessage");

            // Act and Assert
            Assert.IsFalse(testMsg.ShouldExecute());
        }

        [TestMethod]
        public void ReturnTrue_AfterDelayExpires()
        {
            // Arrange
            var fakeClock = A.Fake<IClock>();

            var startTime = new DateTime(2018, 6, 28, 12, 0, 0);

            A.CallTo(() => fakeClock.UtcNow).Returns(startTime).Once()
                .Then.Returns(startTime.AddMinutes(Delay));

            var testMsg = new RepeatingMessage("channel", "Hi, this is a timed message from AlphaBot!", Delay, null, fakeClock, "TestMessage");

            // Act and Assert
            Assert.IsTrue(testMsg.ShouldExecute());
        }

        [TestMethod]
        public void ReturnTrue_AfterExpiryOfSecondDelay()
        {
            // Arrange
            var fakeClock = A.Fake<IClock>();

            var startTime = new DateTime(2018, 6, 28, 12, 0, 0);

            A.CallTo(() => fakeClock.UtcNow).Returns(startTime).Once()
                .Then.Returns(startTime.AddMinutes(2 * Delay));

            var testMsg = new RepeatingMessage("channel", "Hi, this is a timed message from AlphaBot!", Delay, null, fakeClock, "TestMessage");

            // Act and Assert
            Assert.IsTrue(testMsg.ShouldExecute());
        }
    }
}
