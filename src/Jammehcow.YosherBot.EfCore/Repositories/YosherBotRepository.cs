using Jammehcow.YosherBot.EfCore.Models;
using MayBee;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.EfCore.Repositories
{
    public class YosherBotRepository : Repository<YosherBotContext>
    {
        private readonly ILogger<YosherBotRepository> _logger;

        public YosherBotRepository(ILogger<YosherBotRepository> logger, YosherBotContext dbContext) : base(dbContext)
        {
            _logger = logger;
        }

        /// <summary>
        /// Attempt to fetch a single guild with the given Discord snowflake ID
        /// </summary>
        /// <param name="snowflakeId">Discord's snowflake ID of the Guild</param>
        /// <returns>A Maybe either containing the Guild or none if not found</returns>
        public Maybe<Guild> GetGuildBySnowflakeId(ulong snowflakeId)
        {
            return Context.Guilds.SingleAsMaybe(guild => guild.GuildSnowflake == snowflakeId);
        }

        /// <summary>
        /// Attempt to get a user's color role using the user and guild's snowflake IDs
        /// </summary>
        /// <param name="userSnowflake">Discord's snowflake ID of the user</param>
        /// <param name="guildSnowflake">Discord's snowflake ID of the guild</param>
        /// <returns>A Maybe either containing the UserColorRole or none if not found</returns>
        public Maybe<UserColorRole> GetUserColorRole(ulong userSnowflake, ulong guildSnowflake)
        {
            // TODO: clean up nullability of Guild as it'll be a runtime error instead of something the compiler
            // can see
            return Context.UserColorRoles
                .Include(ucr => ucr.Guild)
                .SingleAsMaybe(ucr =>
                    ucr.Guild?.GuildSnowflake == guildSnowflake &&
                    ucr.UserSnowflake == userSnowflake);
        }
    }
}
