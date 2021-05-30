using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Jammehcow.YosherBot.EfCore.Migrations
{
    public partial class UserColorRoles_CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColorRoleStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorRoleStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserColorRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserSnowflake = table.Column<long>(type: "bigint", nullable: false),
                    RoleDisplayName = table.Column<string>(type: "text", nullable: false),
                    RoleSnowflake = table.Column<long>(type: "bigint", nullable: false),
                    GuildId = table.Column<int>(type: "integer", nullable: false),
                    ColorHexCode = table.Column<string>(type: "text", nullable: false),
                    ColorRoleStatusId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateRemoved = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserColorRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserColorRoles_ColorRoleStatuses_ColorRoleStatusId",
                        column: x => x.ColorRoleStatusId,
                        principalTable: "ColorRoleStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserColorRoles_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ColorRoleStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Assigned" },
                    { 2, "Deleted" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserColorRoles_ColorRoleStatusId",
                table: "UserColorRoles",
                column: "ColorRoleStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserColorRoles_GuildId",
                table: "UserColorRoles",
                column: "GuildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserColorRoles");

            migrationBuilder.DropTable(
                name: "ColorRoleStatuses");
        }
    }
}
