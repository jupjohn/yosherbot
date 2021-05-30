using Jammehcow.YosherBot.EfCore.Models;
using MayBee;
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
        public Maybe<Guild> GetGuildBySnowflakeId(long snowflakeId)
        {
            return Context.Guilds.SingleAsMaybe(guild => guild.Id == snowflakeId);
        }
    }
}
