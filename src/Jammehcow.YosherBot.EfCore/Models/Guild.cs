using System;
using Jammehcow.YosherBot.EfCore.Models.Base;

namespace Jammehcow.YosherBot.EfCore.Models
{
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
    }
}
