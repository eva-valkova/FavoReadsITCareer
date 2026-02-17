using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoReads.Migrations
{
    /// <inheritdoc />
    public partial class removeGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Book");

            migrationBuilder.AlterColumn<string>(
                name: "BookReview",
                table: "BookListReader",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 1,
                column: "AverageRating",
                value: 4.5);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 2,
                column: "AverageRating",
                value: 4.4000000000000004);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 3,
                column: "AverageRating",
                value: 4.5999999999999996);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 4,
                column: "AverageRating",
                value: 4.7999999999999998);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 5,
                column: "AverageRating",
                value: 4.9000000000000004);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 6,
                column: "AverageRating",
                value: 4.2999999999999998);

            migrationBuilder.UpdateData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 1,
                column: "BookRating",
                value: 4.5);

            migrationBuilder.UpdateData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 2,
                column: "BookRating",
                value: 5.0);

            migrationBuilder.UpdateData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 3,
                column: "BookRating",
                value: 4.7999999999999998);

            migrationBuilder.UpdateData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 4,
                column: "BookRating",
                value: 4.2999999999999998);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BookReview",
                table: "BookListReader",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Book",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 1,
                columns: new[] { "AverageRating", "Genre" },
                values: new object[] { 4.5m, "Contemporary Romance" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 2,
                columns: new[] { "AverageRating", "Genre" },
                values: new object[] { 4.4m, "Contemporary Romance" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 3,
                columns: new[] { "AverageRating", "Genre" },
                values: new object[] { 4.6m, "Contemporary Romance" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 4,
                columns: new[] { "AverageRating", "Genre" },
                values: new object[] { 4.8m, "Classic Romance" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 5,
                columns: new[] { "AverageRating", "Genre" },
                values: new object[] { 4.9m, "Fantasy" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 6,
                columns: new[] { "AverageRating", "Genre" },
                values: new object[] { 4.3m, "Contemporary Romance" });

            migrationBuilder.UpdateData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 1,
                column: "BookRating",
                value: 4.5m);

            migrationBuilder.UpdateData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 2,
                column: "BookRating",
                value: 5.0m);

            migrationBuilder.UpdateData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 3,
                column: "BookRating",
                value: 4.8m);

            migrationBuilder.UpdateData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 4,
                column: "BookRating",
                value: 4.3m);
        }
    }
}
