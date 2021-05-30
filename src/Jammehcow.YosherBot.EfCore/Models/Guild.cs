using System;
using Discord;
using Jammehcow.YosherBot.EfCore.Models.Base;
using StatusGeneric;

namespace Jammehcow.YosherBot.EfCore.Models
{
    /// <summary>
    /// Represents a Discord Guild
    /// </summary>
    public class Guild : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// The snowflake ID of the guild
        /// </summary>
        public ulong GuildId { get; internal set; }

        /// <summary>
        /// The date this guild was added/joined (UTC)
        /// </summary>
        public DateTime DateAdded { get; internal set; }

        /// <summary>
        /// The date this guild was left (UTC)
        /// </summary>
        public DateTime? DateRemoved { get; internal set; }

        internal Guild(ulong guildId, DateTime dateAdded, DateTime? dateRemoved = null)
        {
            GuildId = guildId;
            DateAdded = dateAdded;
            DateRemoved = dateRemoved;
        }

        /// <summary>
        /// Create a new Guild entity from the given IGuild
        /// </summary>
        /// <param name="sourceGuild">The Discord Guild this entity represents</param>
        /// <param name="dateJoined">The date this Guild was joined</param>
        /// <returns></returns>
        public IStatusGeneric CreateGuildFromDiscordBuild(IGuild sourceGuild, DateTime dateJoined)
        {
            // Will leave as StatusGeneric for now as Guild will end up with some properties
            // that need checking before creation
            var statusHandler = new StatusGenericHandler<Guild>();
            statusHandler.SetResult(new Guild(sourceGuild.Id, dateJoined));

            return statusHandler;
        }
    }
}
