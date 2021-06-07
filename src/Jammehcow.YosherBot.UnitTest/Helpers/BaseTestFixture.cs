using System;
using System.Threading.Tasks;
using Jammehcow.YosherBot.EfCore;
using Jammehcow.YosherBot.EfCore.Repositories;
using Jammehcow.YosherBot.UnitTest.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Jammehcow.YosherBot.UnitTest.Helpers
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public abstract class BaseTestFixture : IDisposable
    {
        private readonly SqliteConnection _sqliteConnection = new("Filename=:memory:");
        private readonly DbContextOptions _options;

        protected YosherBotTestContext Context;
        public YosherBotRepository Repository => new(Mock.Of<ILogger<YosherBotRepository>>(), Context);

        protected BaseTestFixture()
        {
            _sqliteConnection.Open();
            _options = new DbContextOptionsBuilder<YosherBotContext>()
                .UseSqlite(_sqliteConnection)
                .Options;
        }

        private YosherBotTestContext CreateContext() => new(Mock.Of<ILoggerFactory>(), _options);
        public void RefreshContext() => Context = CreateContext();

        // I'm wondering if a solution to not being able to have tests run in parallel is to pool the contexts
        // At runtime (create a list and push connections+contexts onto them) and just let them dispose via Dispose()
        [OneTimeSetUp]
        public async Task SetUpBase()
        {
            Context = CreateContext();
            await Context.Database.EnsureCreatedAsync();
            await SetupContextAsync();
            await Context.SaveChangesAsync();
        }

        protected abstract Task SetupContextAsync();

        public void Dispose() => _sqliteConnection.Dispose();
    }
}
