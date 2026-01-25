using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FavoReads.Migrations
{
    /// <inheritdoc />
    public partial class seedingreadersandlists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookListAuthor",
                columns: new[] { "BookListAuthorID", "AuthorID", "BookID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 2, 4 },
                    { 5, 3, 5 },
                    { 6, 4, 6 }
                });

            migrationBuilder.InsertData(
                table: "Reader",
                columns: new[] { "ReaderID", "Age", "Email", "FirstName", "LastName", "NumberOfReadBooks", "Password", "ProfilePictureUrl" },
                values: new object[,]
                {
                    { 1, 18, "eva.valkova.1003@gmail.com", "Eva", "Valkova", 254, "Coballoway", "https://media.licdn.com/dms/image/D4D03AQH1b0n1bX2m5g/profile-displayphoto-shrink_800_800/0/1683296144862?e=2147483647&v=beta&t=YlKXJ1d8YxY1G3E5KXc1Y3nU6kq5r0F1b4r3F4Z3K2o" },
                    { 2, 25, "eva.n.valkova@gmail.com", "Eva N.", "Valkova", 102, "PrideAndPrejudice", "https://media.licdn.com/dms/image/D4D03AQH1b0n1bX2m5g/profile-displayphoto-shrink_800_800/0/1683296144862?e=2147483647&v=beta&t=YlKXJ1d8YxY1G3E5KXc1Y3nU6kq5r0F1b4r3F4Z3K2o" }
                });

            migrationBuilder.InsertData(
                table: "BookListReader",
                columns: new[] { "BookListReaderID", "BookID", "BookRating", "BookReview", "ReaderID" },
                values: new object[,]
                {
                    { 1, 1, 4.5m, "An intense and gripping read that delves deep into the complexities of addiction and love. The characters are well-developed, and the chemistry between Lily and Loren is palpable. A must-read for fans of contemporary romance.", 1 },
                    { 2, 4, 5.0m, "A timeless classic that beautifully captures the nuances of love, social class, and personal growth. Elizabeth Bennet is a strong and relatable heroine, and Mr. Darcy's transformation is compelling. Austen's wit and keen observations make this novel a joy to read.", 1 },
                    { 3, 5, 4.8m, "A magical journey that captivates readers of all ages. J.K. Rowling's world-building is exceptional, and the characters are memorable and endearing. The story of friendship, bravery, and self-discovery is beautifully told. A fantastic start to an iconic series.", 1 },
                    { 4, 6, 4.3m, "A steamy and emotionally charged romance that explores the complexities of rivalry and attraction. The chemistry between Shane and Ilya is electric, and their journey towards understanding and acceptance is heartwarming. Rachel Reid delivers a compelling story that keeps readers hooked.", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookListAuthor",
                keyColumn: "BookListAuthorID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookListAuthor",
                keyColumn: "BookListAuthorID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookListAuthor",
                keyColumn: "BookListAuthorID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookListAuthor",
                keyColumn: "BookListAuthorID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookListAuthor",
                keyColumn: "BookListAuthorID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BookListAuthor",
                keyColumn: "BookListAuthorID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookListReader",
                keyColumn: "BookListReaderID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reader",
                keyColumn: "ReaderID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reader",
                keyColumn: "ReaderID",
                keyValue: 1);
        }
    }
}
