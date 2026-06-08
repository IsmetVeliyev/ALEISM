using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalStudentProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddReplyToIdToMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplyToId",
                table: "SubMessages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReplyToId",
                table: "Messages",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyToId",
                table: "SubMessages");

            migrationBuilder.DropColumn(
                name: "ReplyToId",
                table: "Messages");
        }
    }
}
