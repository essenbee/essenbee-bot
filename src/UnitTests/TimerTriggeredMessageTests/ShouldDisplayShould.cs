using System;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
using Essenbee.Bot.Core;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.TimerTriggeredMessageTests
{
    [TestClass]
    public class ShouldDisplayShould
    {
        private const int Delay = 1;

        [TestMethod]
        public void ReturnFalse_WhenCreated()
        {
            //Arrange
            var fakeClock = A.Fake<IClock>();

            A.CallTo(() => fakeClock.Now).Returns(new DateTime(2018, 6, 28, 12, 0, 0));

            var testMsg = new TimerTriggeredMessage
            {
                Delay = Delay,
                Message = "Hi, this is a timed message from CoreBot!"
            };

            testMsg.Init(fakeClock.Now, ItemStatus.Active);
            
            // Act and Assert
            Assert.IsFalse(testMsg.ShouldDisplay(fakeClock.Now));
        }

        [TestMethod]
        public void ReturnFalse_AfterHalfDelayExpired()
        {
            //Arrange
            var fakeClock = A.Fake<IClock>();

            A.CallTo(() => fakeClock.Now).Returns(new DateTime(2018, 6, 28, 12, 0, 0));

            var testMsg = new TimerTriggeredMessage
            {
                Delay = Delay,
                Message = "Hi, this is a timed message from CoreBot!"
            };

            testMsg.Init(fakeClock.Now, ItemStatus.Active);

            // Act and Assert
            Assert.IsFalse(testMsg.ShouldDisplay(fakeClock.Now.AddSeconds(30)));
        }

        [TestMethod]
        public void ReturnTrue_AfterDelayExpires()
        {
            // Arrange
            var fakeClock = A.Fake<IClock>();

            A.CallTo(() => fakeClock.Now).Returns(new DateTime(2018, 6, 28, 12, 0, 0));

            var testMsg = new TimerTriggeredMessage
            {
                Delay = Delay,
                Message = "Hi, this is a timed message from CoreBot!"
            };

            testMsg.Init(fakeClock.Now, ItemStatus.Active);

            // Act and Assert
            Assert.IsTrue(testMsg.ShouldDisplay(fakeClock.Now.AddMinutes(Delay)));
        }

        [TestMethod]
        public void ReturnFalse_ImmediatelyAfterGetMessage()
        {
            // Arrange
            var fakeClock = A.Fake<IClock>();

            A.CallTo(() => fakeClock.Now).Returns(new DateTime(2018, 6, 28, 12, 0, 0));

            var testMsg = new TimerTriggeredMessage
            {
                Delay = Delay,
                Message = "Hi, this is a timed message from CoreBot!"
            };

            testMsg.Init(fakeClock.Now, ItemStatus.Active);
            testMsg.GetMessage(fakeClock.Now.AddMinutes(Delay)); // Will reset the timer

            // Act and Assert
            Assert.IsFalse(testMsg.ShouldDisplay(fakeClock.Now.AddMinutes(Delay)));
        }

        [TestMethod]
        public void ReturnTrue_AfterExpiryOfSecondDelay()
        {
            // Arrange
            var fakeClock = A.Fake<IClock>();

            A.CallTo(() => fakeClock.Now).Returns(new DateTime(2018, 6, 28, 12, 0, 0));

            var testMsg = new TimerTriggeredMessage
            {
                Delay = Delay,
                Message = "Hi, this is a timed message from CoreBot!"
            };

            testMsg.GetMessage(fakeClock.Now.AddMinutes(1));

            // Act and Assert
            Assert.IsTrue(testMsg.ShouldDisplay(fakeClock.Now.AddMinutes(2 * Delay)));
        }
    }
}
