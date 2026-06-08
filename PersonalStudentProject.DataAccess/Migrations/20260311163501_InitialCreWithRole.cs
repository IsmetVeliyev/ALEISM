using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalStudentProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreWithRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderMail",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "SenderEmail",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderEmail",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "SenderMail",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
