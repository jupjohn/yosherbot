using System;
using Jammehcow.YosherBot.Console;
using NUnit.Framework;

namespace Jammehcow.YosherBot.UnitTest
{
    [TestFixture]
    public class BotStartupTests
    {
        [Test]
        public void ShouldFactoryThrow_WhenBotTokenMissing()
        {
            TestHelper.SetBotToken(null);
            Assert.Throws(typeof(ArgumentException), () => BotStartup.CreateDefaultBot());
        }

        [Test]
        public void ShouldFactoryReturn_WhenBotTokenProvided()
        {
            TestHelper.SetBotToken("abc");
            Assert.DoesNotThrow(() => BotStartup.CreateDefaultBot());
        }
    }
}