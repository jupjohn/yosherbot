using Microsoft.EntityFrameworkCore.Migrations;

namespace Jammehcow.YosherBot.EfCore.Migrations
{
    public partial class UserColorRole_RenameStatusIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserColorRoles_ColorRoleStatuses_ColorRoleStatusId",
                table: "UserColorRoles");

            migrationBuilder.RenameColumn(
                name: "ColorRoleStatusId",
                table: "UserColorRoles",
                newName: "RoleStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_UserColorRoles_ColorRoleStatusId",
                table: "UserColorRoles",
                newName: "IX_UserColorRoles_RoleStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserColorRoles_ColorRoleStatuses_RoleStatusId",
                table: "UserColorRoles",
                column: "RoleStatusId",
                principalTable: "ColorRoleStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserColorRoles_ColorRoleStatuses_RoleStatusId",
                table: "UserColorRoles");

            migrationBuilder.RenameColumn(
                name: "RoleStatusId",
                table: "UserColorRoles",
                newName: "ColorRoleStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_UserColorRoles_RoleStatusId",
                table: "UserColorRoles",
                newName: "IX_UserColorRoles_ColorRoleStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserColorRoles_ColorRoleStatuses_ColorRoleStatusId",
                table: "UserColorRoles",
                column: "ColorRoleStatusId",
                principalTable: "ColorRoleStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
