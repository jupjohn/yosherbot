using Jammehcow.YosherBot.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.UnitTest.Contexts
{
    public class YosherBotTestContext : YosherBotContext
    {
        public YosherBotTestContext(ILoggerFactory loggerFactoryFactory, DbContextOptions options) : base(
            loggerFactoryFactory, options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Prevent the base OnConfiguring from being called
        }
    }
}
