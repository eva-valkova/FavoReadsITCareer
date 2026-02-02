using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoReads.Migrations
{
    /// <inheritdoc />
    public partial class IdentityLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Reader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Author",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "AuthorID",
                keyValue: 1,
                column: "IdentityUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "AuthorID",
                keyValue: 2,
                column: "IdentityUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "AuthorID",
                keyValue: 3,
                column: "IdentityUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Author",
                keyColumn: "AuthorID",
                keyValue: 4,
                column: "IdentityUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reader",
                keyColumn: "ReaderID",
                keyValue: 1,
                column: "IdentityUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reader",
                keyColumn: "ReaderID",
                keyValue: 2,
                column: "IdentityUserId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Reader");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Author");
        }
    }
}
