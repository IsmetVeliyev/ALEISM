using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalStudentProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Il : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubRooms_SubRooms_SubRoomId",
                table: "UserSubRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubRooms_Users_UserId",
                table: "UserSubRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubRooms",
                table: "UserSubRooms");

            migrationBuilder.RenameTable(
                name: "UserSubRooms",
                newName: "UserSubRms");

            migrationBuilder.RenameIndex(
                name: "IX_UserSubRooms_SubRoomId",
                table: "UserSubRms",
                newName: "IX_UserSubRms_SubRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubRms",
                table: "UserSubRms",
                columns: new[] { "UserId", "SubRoomId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubRms_SubRooms_SubRoomId",
                table: "UserSubRms",
                column: "SubRoomId",
                principalTable: "SubRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubRms_Users_UserId",
                table: "UserSubRms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubRms_SubRooms_SubRoomId",
                table: "UserSubRms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubRms_Users_UserId",
                table: "UserSubRms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubRms",
                table: "UserSubRms");

            migrationBuilder.RenameTable(
                name: "UserSubRms",
                newName: "UserSubRooms");

            migrationBuilder.RenameIndex(
                name: "IX_UserSubRms_SubRoomId",
                table: "UserSubRooms",
                newName: "IX_UserSubRooms_SubRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubRooms",
                table: "UserSubRooms",
                columns: new[] { "UserId", "SubRoomId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubRooms_SubRooms_SubRoomId",
                table: "UserSubRooms",
                column: "SubRoomId",
                principalTable: "SubRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubRooms_Users_UserId",
                table: "UserSubRooms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
