using System;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: pass in from startup, not here
            var connectionString = Environment.GetEnvironmentVariable("YosherBot__DbConnectionString");
            if (connectionString == null)
                throw new ArgumentException("Connection string was null");

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnumEntity<ColorRoleStatusEnum, ColorRoleStatus>();
        }
    }
}
