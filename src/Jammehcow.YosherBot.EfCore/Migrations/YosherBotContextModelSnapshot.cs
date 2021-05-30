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

            modelBuilder.Entity("Jammehcow.YosherBot.EfCore.Models.Guild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateRemoved")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("GuildId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });
#pragma warning restore 612, 618
        }
    }
}