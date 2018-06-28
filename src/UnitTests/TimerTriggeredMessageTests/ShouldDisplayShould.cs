using System;
using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Interfaces;
using Essenbee.Bot.Core.Messaging;
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
            var fakeClock = A.Fake<IClock>();

            A.CallTo(() => fakeClock.Now).Returns(new DateTime(2018, 6, 28, 12, 0, 0));

            var testMsg = new TimerTriggeredMessage
            {
                Delay = Delay,
                Message = "Hi, this is a timed message from CoreBot!"
            };

            testMsg.Init(fakeClock.Now);
            
            Assert.IsFalse(testMsg.ShouldDisplay(fakeClock.Now));
        }
    }
}
