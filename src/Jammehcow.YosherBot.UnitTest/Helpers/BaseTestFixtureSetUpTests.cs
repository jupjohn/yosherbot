using System;
using System.Threading.Tasks;
using Jammehcow.YosherBot.EfCore.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Jammehcow.YosherBot.UnitTest.Helpers
{
    public class BaseTestFixtureSetUpTests : BaseTestFixture
    {
        [Test]
        public async Task DoesSetupContextSaveChanges()
        {
            var guilds = await Context.Guilds.ToListAsync();
            Assert.IsNotEmpty(guilds);
        }

        [Test]
        public async Task DoesDiscardUnsavedChangesWhenRefreshed()
        {
            const int newGuildSnowflake = 22;
            await Context.Guilds.AddAsync(new Guild(newGuildSnowflake, DateTime.Now));

            RefreshContext();

            var savedGuild = await Context.Guilds.FirstOrDefaultAsync(g => g.GuildSnowflake == newGuildSnowflake);
            Assert.IsNull(savedGuild, "An unsaved entity was persisted to the DB after refresh");
        }

        protected override async Task SetupContextAsync()
        {
            await Context.Guilds.AddAsync(new Guild(1, DateTime.Now));
        }
    }
}
