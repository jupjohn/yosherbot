using System;
using Jammehcow.YosherBot.Common.Helpers.Environment;
using Jammehcow.YosherBot.EfCore.Enums;
using Jammehcow.YosherBot.EfCore.Extensions;
using Jammehcow.YosherBot.EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.EfCore
{
    public class YosherBotContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        #region Entities

        public virtual DbSet<Guild> Guilds { get; set; } = null!;
        public virtual DbSet<UserColorRole> UserColorRoles { get; set; } = null!;

        #endregion

        #region Enums

        public virtual DbSet<ColorRoleStatus> ColorRoleStatuses { get; set; } = null!;

        #endregion

        public YosherBotContext(ILoggerFactory loggerFactoryFactory) => _loggerFactory = loggerFactoryFactory;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: pass in from startup, not here
            var connectionString = Environment.GetEnvironmentVariable("YosherBot__DbConnectionString");
            if (connectionString == null)
                throw new ArgumentException("Connection string was null");

            if (EnvironmentsHelper.IsDevelopment())
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }

            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .UseNpgsql(connectionString, builder => builder.CommandTimeout(10));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnumEntity<ColorRoleStatusEnum, ColorRoleStatus>();
        }
    }
}
