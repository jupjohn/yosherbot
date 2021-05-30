using Microsoft.EntityFrameworkCore.Migrations;

namespace Jammehcow.YosherBot.EfCore.Migrations
{
    public partial class Snowflakes_UlongNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuildId",
                table: "Guilds",
                newName: "GuildSnowflake");

            migrationBuilder.AlterColumn<decimal>(
                name: "UserSnowflake",
                table: "UserColorRoles",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "RoleSnowflake",
                table: "UserColorRoles",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuildSnowflake",
                table: "Guilds",
                newName: "GuildId");

            migrationBuilder.AlterColumn<long>(
                name: "UserSnowflake",
                table: "UserColorRoles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AlterColumn<long>(
                name: "RoleSnowflake",
                table: "UserColorRoles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");
        }
    }
}
