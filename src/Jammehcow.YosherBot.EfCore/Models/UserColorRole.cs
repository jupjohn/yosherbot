using System;
using System.ComponentModel.DataAnnotations;
using Discord;
using Jammehcow.YosherBot.Common.Models;
using Jammehcow.YosherBot.EfCore.Enums;
using Jammehcow.YosherBot.EfCore.Models.Base;
using StatusGeneric;

namespace Jammehcow.YosherBot.EfCore.Models
{
    // TODO: audit history of color/display name changes
    public class UserColorRole : IEntity
    {
        public int Id { get; init; }

        /// <summary>
        /// The Discord ID of the user this role is applied to
        /// </summary>
        public ulong UserSnowflake { get; internal set; }

        /// <summary>
        /// The display name of the role
        /// </summary>
        [Required]
        public string RoleDisplayName { get; internal set; }

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
        public string ColorHexCode { get; internal set; }

        /// <summary>
        /// The status of this color role
        /// </summary>
        [Required]
        public int RoleStatusId { get; internal set; }
        public virtual ColorRoleStatus RoleStatus { get; internal set; } = null!;

        /// <summary>
        /// The date this role was applied
        /// </summary>
        public DateTime DateCreated { get; internal set; }

        /// <summary>
        /// The date this role was applied
        /// </summary>
        public DateTime? DateRemoved { get; internal set; }

        internal UserColorRole(ulong userSnowflake, string roleDisplayName, ulong roleSnowflake, int guildId,
            string colorHexCode, int roleStatusId, DateTime dateCreated)
        {
            UserSnowflake = userSnowflake;
            RoleDisplayName = roleDisplayName;
            RoleSnowflake = roleSnowflake;
            GuildId = guildId;
            ColorHexCode = colorHexCode;
            RoleStatusId = roleStatusId;
            DateCreated = dateCreated;
        }

        public static IStatusGeneric<UserColorRole> CreateUserColorRole(IUser user, IRole colorRole, Guild guild,
            ColorModel color, string? roleDisplayName = null, DateTime? dateCreated = null)
        {
            var status = new StatusGenericHandler<UserColorRole>();

            if (guild.DateRemoved != null)
                return status.AddError("Unable to create role for a removed guild");

            var entity = new UserColorRole(user.Id, roleDisplayName ?? user.Username, colorRole.Id, guild.Id,
                color.HexCode, (int) ColorRoleStatusEnum.Assigned, dateCreated ?? DateTime.Now);
            return status.SetResult(entity);
        }

        /// <summary>
        /// Set the display name of the color role
        /// </summary>
        /// <param name="newDisplayName">The display name to replace the current name</param>
        /// <returns>A status containing errors (if any)</returns>
        public IStatusGeneric SetDisplayName(string newDisplayName)
        {
            var statusHandler = new StatusGenericHandler();

            if (string.IsNullOrWhiteSpace(newDisplayName))
                return statusHandler.AddError("The new display name must contain characters");

            RoleDisplayName = newDisplayName;
            return statusHandler;
        }

        /// <summary>
        /// Sets the color of the role
        /// </summary>
        /// <param name="color">The color to set the role to</param>
        /// <returns>A status containing errors (if any)</returns>
        public IStatusGeneric SetRoleColor(ColorModel color)
        {
            var statusHandler = new StatusGenericHandler();

            if (color.HexCode == ColorHexCode)
                return statusHandler.AddError($"Color was already set to {ColorHexCode}");

            ColorHexCode = color.HexCode;
            return statusHandler;
        }

        /// <summary>
        /// Sets the status of this role to removed as well as providing the date of removal
        /// </summary>
        /// <param name="dateRemoved">The date that this role was removed on</param>
        /// <returns>A status containing errors (if any)</returns>
        public IStatusGeneric MarkAsDeleted(DateTime dateRemoved)
        {
            var statusHandler = new StatusGenericHandler();

            // Don't return here as it can chain with the next check
            if (DateRemoved != null)
                statusHandler.AddError("DateRemoved is already set!", nameof(DateRemoved));

            if (RoleStatusId == (int) ColorRoleStatusEnum.Deleted)
                return statusHandler.AddError("Role status is already deleted", nameof(RoleStatusId));

            if (dateRemoved < DateCreated)
                return statusHandler.AddError("DateRemoved cannot be before DateCreated", nameof(DateRemoved));

            DateRemoved = dateRemoved;
            return statusHandler;
        }
    }
}
