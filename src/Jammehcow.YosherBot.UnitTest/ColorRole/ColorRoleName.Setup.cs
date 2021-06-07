using System;
using System.Threading.Tasks;
using Jammehcow.YosherBot.EfCore.Enums;
using Jammehcow.YosherBot.EfCore.Models;

namespace Jammehcow.YosherBot.UnitTest.ColorRole
{
    public partial class ColorRoleName
    {
        private const ulong GuildActiveOneSnowflake = 1;
        private const ulong GuildActiveTwoSnowflake = 2;
        private const ulong GuildInactiveOneSnowflake = 3;

        private const ulong UserSnowflakeId = 1;

        private const string RoleInitialName = "Test Role";

        protected override async Task SetupContextAsync()
        {
            // create two Guild entities
            var activeGuildOne = new Guild(GuildActiveOneSnowflake, DateTime.Parse("2021-01-01"));
            await Context.Guilds.AddAsync(activeGuildOne);

            var activeGuildTwo = new Guild(GuildActiveTwoSnowflake, DateTime.Parse("2021-01-02"));
            await Context.Guilds.AddAsync(activeGuildTwo);

            var inactiveGuild = new Guild(GuildInactiveOneSnowflake,
                DateTime.Parse("2021-01-03"), DateTime.Parse("2021-02-01"));
            await Context.Guilds.AddAsync(inactiveGuild);

            await Context.SaveChangesAsync();

            // create a single UserColorRole entity
            var userColorRole = new UserColorRole(UserSnowflakeId, RoleInitialName, 0, activeGuildOne.Id, "#000000",
                (int) ColorRoleStatusEnum.Assigned, activeGuildOne.DateAdded.AddDays(1));
            await Context.UserColorRoles.AddAsync(userColorRole);
        }
    }
}
