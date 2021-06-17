using Jammehcow.YosherBot.Common.Helpers.Environment;
using NUnit.Framework;

namespace Jammehcow.YosherBot.UnitTest
{
    [TestFixture]
    public class EnvironmentsHelperTests
    {
        [Test]
        public void ShouldDevelopmentBeTrue_WhenEnvvarIsUnset()
        {
            TestHelper.SetEnvironmentName(null);

            var result = EnvironmentsHelper.IsDevelopment();
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldProductionBeFalse_WhenEnvvarIsUnset()
        {
            TestHelper.SetEnvironmentName(null);

            var result = EnvironmentsHelper.IsProduction();
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldDevelopmentBeFalse_WhenEnvvarIsProduction()
        {
            TestHelper.SetEnvironmentName("production");

            var result = EnvironmentsHelper.IsDevelopment();
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldProductionBeTrue_WhenEnvvarIsProduction()
        {
            TestHelper.SetEnvironmentName("production");

            var result = EnvironmentsHelper.IsProduction();
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldDevelopmentBeFalse_WhenEnvvarIsUnknown()
        {
            TestHelper.SetEnvironmentName("abc");

            var result = EnvironmentsHelper.IsDevelopment();
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldProductionBeFalse_WhenEnvvarIsUnknown()
        {
            TestHelper.SetEnvironmentName("abc");

            var result = EnvironmentsHelper.IsProduction();
            Assert.IsFalse(result);
        }
    }
}