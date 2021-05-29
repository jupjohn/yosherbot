using Jammehcow.YosherBot.EfCore.Enums;
using Jammehcow.YosherBot.EfCore.Extensions;
using Jammehcow.YosherBot.EfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Jammehcow.YosherBot.EfCore
{
    public class YosherBotContext : DbContext
    {
        #region Entities

        public virtual DbSet<Guild> Guilds { get; set; } = null!;
        public virtual DbSet<UserColorRole> UserColorRoles { get; set; } = null!;

        #endregion

        #region Enums

        public virtual DbSet<ColorRoleStatus> ColorRoleStatuses { get; set; } = null!;

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.EnumEntity<ColorRoleStatusEnum, ColorRoleStatus>();
        }
    }
}
