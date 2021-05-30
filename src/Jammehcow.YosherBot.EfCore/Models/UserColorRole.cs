using System;
using System.ComponentModel.DataAnnotations;
using Jammehcow.YosherBot.EfCore.Models.Base;

namespace Jammehcow.YosherBot.EfCore.Models
{
    public class UserColorRole : IEntity
    {
        int IEntity.Id { get; init; }

        /// <summary>
        /// The Discord ID of the user this role is applied to
        /// </summary>
        public ulong UserSnowflake { get; internal set; }

        /// <summary>
        /// The display name of the role
        /// </summary>
        [Required]
        public string RoleDisplayName { get; internal set; } = null!;

        /// <summary>
        /// The Discord ID of the Guild role
        /// </summary>
        public ulong RoleSnowflake { get; internal set; }

        /// <summary>
        /// The ID of the guild
        /// </summary>
        public int GuildId { get; internal set; }
        public virtual Guild? Guild { get; internal set; }

        /// <summary>
        /// The hex code of the role's color
        /// </summary>
        [Required]
        public string ColorHexCode { get; internal set; } = null!;

        /// <summary>
        /// The status of this color role
        /// </summary>
        [Required]
        public int ColorRoleStatusId { get; internal set; }
        public virtual ColorRoleStatus RoleStatus { get; internal set; } = null!;

        /// <summary>
        /// The date this role was applied
        /// </summary>
        public DateTime DateCreated { get; internal set; }

        /// <summary>
        /// The date this role was applied
        /// </summary>
        public DateTime DateRemoved { get; internal set; }
    }
}
