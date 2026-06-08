using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalStudentProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteSubRoomMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubRooms_SubRooms_SubRoomId",
                table: "UserSubRooms");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubRooms_SubRooms_SubRoomId",
                table: "UserSubRooms",
                column: "SubRoomId",
                principalTable: "SubRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubRooms_SubRooms_SubRoomId",
                table: "UserSubRooms");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubRooms_SubRooms_SubRoomId",
                table: "UserSubRooms",
                column: "SubRoomId",
                principalTable: "SubRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
