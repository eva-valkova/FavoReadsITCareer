using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FavoReads.Migrations
{
    /// <inheritdoc />
    public partial class SeedingBooksAndAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "AuthorID", "Age", "Biography", "Email", "FirstName", "LastName", "Password", "ProfilePictureUrl" },
                values: new object[,]
                {
                    { 1, 31, "Krista and Becca Ritchie are bestselling authors known for their contemporary romance novels. They have co-authored several popular series, including the 'Addicted' series and the 'Calloway Sisters' series. Their books often explore themes of love, friendship, and personal growth, and they have garnered a dedicated fanbase for their engaging storytelling and relatable characters.", "KBR@email.com", "Krista and Becca", "Ritchie", "Lilo", "https://static.wixstatic.com/media/bf6fdf_04623ef9a1f24586b3a082140c6e59e7~mv2.jpg/v1/crop/x_896,y_0,w_3209,h_3335/fill/w_253,h_263,al_c,q_80,usm_0.66_1.00_0.01,enc_avif,quality_auto/Krista%20and%20Becca%20-%20Author%20Photo%202.jpg" },
                    { 2, 41, "Jane Austen was an English novelist known primarily for her six major novels, which interpret, critique and comment upon the British landed gentry at the end of the 18th century. Austen's plots often explore", "jane.austen@email.com", "Jane", "Austen", "ElizabethDarcy", "https://cdn.britannica.com/12/172012-050-DAA7CE6B/Jane-Austen-Cassandra-engraving-portrait-1810.jpg" },
                    { 3, 58, "J.K. Rowling is a British author best known for writing the Harry Potter fantasy series, which has won multiple awards and sold more than 500 million copies worldwide, making it the best-selling book series in history. The books have been adapted into a popular film series, further expanding Rowling's influence in popular culture. Beyond Harry Potter, she has written novels for adults under the pseudonym Robert Galbraith.", "jk.rowling@email.com", "J.K.", "Rowling", "Romione", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRq56NpoBJCRzrdQx2dftEAll-WVStdDCCAgw&s" },
                    { 4, 29, "Rachel Reid is a contemporary romance author known for her emotionally charged storytelling and complex characters. She has written several novels that explore themes of love, loss, and personal growth. Reid's work often features strong male hockey players", "rachel.reid@gmail.com", "Rachel", "Reid", "Hollanov", "https://cdn.i-scmp.com/sites/default/files/styles/768x768/public/d8/images/canvas/2025/12/15/3d0392ca-0e60-4d31-a360-21da79d827f0_717ca59e.jpg?itok=zewT6aXn&v=1765791561" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookID", "AuthorID", "AverageRating", "CoverImageUrl", "Description", "Genre", "Title" },
                values: new object[,]
                {
                    { 1, 1, 4.5m, "https://cdn2.penguin.com.au/covers/original/9780593549476.jpg", "Addicted to You is a contemporary romance novel by Krista and Becca Ritchie. It is the first book in the Addicted series and follows the story of Lily Calloway and Loren Hale as they navigate their complicated relationship, personal struggles, and the challenges of addiction.", "Contemporary Romance", "Addicted To You" },
                    { 2, 1, 4.4m, "https://m.media-amazon.com/images/I/81E1ZJyA-kL._UF1000,1000_QL80_.jpg", "Ricochet is the second book in the Addicted series by Krista and Becca Ritchie. The story continues to follow Lily Calloway and Loren Hale as they deal with the aftermath of the events in Addicted to You, facing new challenges in their relationship and personal lives.", "Contemporary Romance", "Ricochet" },
                    { 3, 1, 4.6m, "https://images-eu.ssl-images-amazon.com/images/I/814gbvG+-RL._AC_UL600_SR600,600_.jpg", "Addicted For Now is the third book in the Addicted series by Krista and Becca Ritchie. The story continues to follow Lily Calloway and Loren Hale as they navigate their complicated relationship, personal struggles, and the challenges of addiction.", "Contemporary Romance", "Addicted For Now" },
                    { 4, 2, 4.8m, "https://bookoutlet.ca/api/image?url=https://images.bookoutlet.com/covers/large/isbn978059/9780593622452-l.jpg&w=3840&q=75", "Pride and Prejudice is a classic novel by Jane Austen that explores themes of love, social class, and individual growth. The story follows Elizabeth Bennet as she navigates societal expectations and her evolving relationship with the proud Mr. Darcy.", "Classic Romance", "Pride and Prejudice" },
                    { 5, 3, 4.9m, "https://upload.wikimedia.org/wikipedia/en/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg", "Harry Potter and the Philosopher's Stone is the first book in the Harry Potter series by J.K. Rowling. It introduces readers to the magical world of Hogwarts and follows young wizard Harry Potter as he discovers his true heritage and begins his journey in the wizarding world.", "Fantasy", "Harry Potter and the Philosopher's Stone" },
                    { 6, 4, 4.3m, "https://m.media-amazon.com/images/I/71iwUtZjamL._UF1000,1000_QL80_.jpg", "Heated Rivalry is a contemporary romance novel by Rachel Reid. The story follows the intense and passionate relationship between two rival hockey players - Shane Hollander and Ilya Rozanov who find themselves drawn to each other despite their competitive nature.", "Contemporary Romance", "Heated Rivalry" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorID",
                keyValue: 4);
        }
    }
}
