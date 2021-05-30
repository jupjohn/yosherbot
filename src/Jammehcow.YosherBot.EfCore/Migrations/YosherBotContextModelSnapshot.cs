﻿// <auto-generated />
using System;
using Jammehcow.YosherBot.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Jammehcow.YosherBot.EfCore.Migrations
{
    [DbContext(typeof(YosherBotContext))]
    partial class YosherBotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Jammehcow.YosherBot.EfCore.Models.ColorRoleStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ColorRoleStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Assigned"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Deleted"
                        });
                });

            modelBuilder.Entity("Jammehcow.YosherBot.EfCore.Models.Guild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateRemoved")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("GuildSnowflake")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("Jammehcow.YosherBot.EfCore.Models.UserColorRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ColorHexCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateRemoved")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("GuildId")
                        .HasColumnType("integer");

                    b.Property<string>("RoleDisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("RoleSnowflake")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("RoleStatusId")
                        .HasColumnType("integer");

                    b.Property<decimal>("UserSnowflake")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("RoleStatusId");

                    b.ToTable("UserColorRoles");
                });

            modelBuilder.Entity("Jammehcow.YosherBot.EfCore.Models.UserColorRole", b =>
                {
                    b.HasOne("Jammehcow.YosherBot.EfCore.Models.Guild", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jammehcow.YosherBot.EfCore.Models.ColorRoleStatus", "RoleStatus")
                        .WithMany()
                        .HasForeignKey("RoleStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guild");

                    b.Navigation("RoleStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
