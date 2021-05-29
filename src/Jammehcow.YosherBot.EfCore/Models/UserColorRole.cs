using System;
using System.ComponentModel.DataAnnotations;
using Jammehcow.YosherBot.EfCore.Models.Base;

namespace Jammehcow.YosherBot.EfCore.Models
{
    public class UserColorRole : IEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// The Discord ID of the user this role is applied to
        /// </summary>
        public long UserSnowflake { get; set; }

        /// <summary>
        /// The display name of the role
        /// </summary>
        [Required]
        public string RoleDisplayName { get; set; } = null!;

        /// <summary>
        /// The Discord ID of the Guild role
        /// </summary>
        public long RoleSnowflake { get; set; }

        /// <summary>
        /// The ID of the guild
        /// </summary>
        public int GuildId { get; set; }
        public virtual Guild? Guild { get; set; }

        /// <summary>
        /// The hex code of the role's color
        /// </summary>
        [Required]
        public string ColorHexCode { get; set; } = null!;

        /// <summary>
        /// The status of this color role
        /// </summary>
        [Required]
        public int ColorRoleStatusId { get; set; }
        public virtual ColorRoleStatus RoleStatus { get; set; } = null!;

        /// <summary>
        /// The date this role was applied
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The date this role was applied
        /// </summary>
        public DateTime DateRemoved { get; set; }
    }
}
