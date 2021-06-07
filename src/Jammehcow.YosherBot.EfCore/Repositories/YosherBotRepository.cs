using System;
using System.Threading.Tasks;
using Discord;
using Jammehcow.YosherBot.EfCore.Extensions;
using Jammehcow.YosherBot.EfCore.Models;
using MayBee;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.EfCore.Repositories
{
    public class YosherBotRepository : Repository<YosherBotContext>
    {
        private readonly ILogger<YosherBotRepository> _logger;

        // TODO: caching
        public YosherBotRepository(ILogger<YosherBotRepository> logger, YosherBotContext dbContext) : base(dbContext)
        {
            _logger = logger;
        }

        /// <summary>
        /// Attempt to fetch a single guild with the given Discord snowflake ID
        /// </summary>
        /// <param name="guild">The Discord guild to search for</param>
        /// <returns>A Maybe either containing the Guild or none if not found</returns>
        public async Task<Guild> GetOrCreateGuildAsync(IGuild guild, DateTime? dateCreated = null)
        {
            var potentialGuild = await Context.Guilds
                .SingleOrDefaultAsync(g =>
                    g.GuildSnowflake == guild.Id &&
                    g.DateRemoved == null);

            if (potentialGuild != null)
                return potentialGuild;

            var guildEntity = Guild.CreateGuildFromDiscordGuild(guild, dateCreated);
            if (guildEntity.HasErrors)
            {
                throw new ArgumentException(
                    $"Guild could not be created for the following reaons: {guildEntity.GetAllErrors(", ")}");
            }

            var entity = await Context.AddAsync(guildEntity.Result);
            return entity.Entity;
        }

        /// <summary>
        /// Attempt to get a user's color role using the user and guild's snowflake IDs
        /// </summary>
        /// <param name="userSnowflake">Discord's snowflake ID of the user</param>
        /// <param name="guildSnowflake">Discord's snowflake ID of the guild</param>
        /// <returns>A Maybe either containing the UserColorRole or none if not found</returns>
        public Task<Maybe<UserColorRole>> GetUserColorRoleAsync(ulong userSnowflake, ulong guildSnowflake)
        {
            // TODO: clean up nullability of Guild as it'll be a runtime error instead of something the compiler
            // can see
            return Context.UserColorRoles
                .Include(ucr => ucr.Guild)
                .SingleAsMaybeAsync(ucr =>
                    ucr.Guild!.GuildSnowflake == guildSnowflake &&
                    ucr.UserSnowflake == userSnowflake);
        }
    }
}
