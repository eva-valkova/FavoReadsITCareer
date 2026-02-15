using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FavoReads.Migrations
{
    /// <inheritdoc />
    public partial class SQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Biography = table.Column<string>(type: "TEXT", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IdentityUserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "Reader",
                columns: table => new
                {
                    ReaderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfReadBooks = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IdentityUserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reader", x => x.ReaderID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorID = table.Column<int>(type: "INTEGER", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Book_Author_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Author",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookListAuthor",
                columns: table => new
                {
                    BookListAuthorID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookID = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookListAuthor", x => x.BookListAuthorID);
                    table.ForeignKey(
                        name: "FK_BookListAuthor_Author_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Author",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookListAuthor_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookListReader",
                columns: table => new
                {
                    BookListReaderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookID = table.Column<int>(type: "INTEGER", nullable: false),
                    ReaderID = table.Column<int>(type: "INTEGER", nullable: false),
                    BookRating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    BookReview = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookListReader", x => x.BookListReaderID);
                    table.ForeignKey(
                        name: "FK_BookListReader_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookListReader_Reader_ReaderID",
                        column: x => x.ReaderID,
                        principalTable: "Reader",
                        principalColumn: "ReaderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "AuthorID", "Age", "Biography", "Email", "FirstName", "IdentityUserId", "LastName", "ProfilePictureUrl" },
                values: new object[,]
                {
                    { 1, 31, "Krista and Becca Ritchie are bestselling authors known for their contemporary romance novels. They have co-authored several popular series, including the 'Addicted' series and the 'Calloway Sisters' series. Their books often explore themes of love, friendship, and personal growth, and they have garnered a dedicated fanbase for their engaging storytelling and relatable characters.", "KBR@email.com", "Krista and Becca", null, "Ritchie", "https://static.wixstatic.com/media/bf6fdf_04623ef9a1f24586b3a082140c6e59e7~mv2.jpg/v1/crop/x_896,y_0,w_3209,h_3335/fill/w_253,h_263,al_c,q_80,usm_0.66_1.00_0.01,enc_avif,quality_auto/Krista%20and%20Becca%20-%20Author%20Photo%202.jpg" },
                    { 2, 41, "Jane Austen was an English novelist known primarily for her six major novels, which interpret, critique and comment upon the British landed gentry at the end of the 18th century. Austen's plots often explore", "jane.austen@email.com", "Jane", null, "Austen", "https://cdn.britannica.com/12/172012-050-DAA7CE6B/Jane-Austen-Cassandra-engraving-portrait-1810.jpg" },
                    { 3, 58, "J.K. Rowling is a British author best known for writing the Harry Potter fantasy series, which has won multiple awards and sold more than 500 million copies worldwide, making it the best-selling book series in history. The books have been adapted into a popular film series, further expanding Rowling's influence in popular culture. Beyond Harry Potter, she has written novels for adults under the pseudonym Robert Galbraith.", "jk.rowling@email.com", "J.K.", null, "Rowling", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRq56NpoBJCRzrdQx2dftEAll-WVStdDCCAgw&s" },
                    { 4, 29, "Rachel Reid is a contemporary romance author known for her emotionally charged storytelling and complex characters. She has written several novels that explore themes of love, loss, and personal growth. Reid's work often features strong male hockey players", "rachel.reid@gmail.com", "Rachel", null, "Reid", "https://cdn.i-scmp.com/sites/default/files/styles/768x768/public/d8/images/canvas/2025/12/15/3d0392ca-0e60-4d31-a360-21da79d827f0_717ca59e.jpg?itok=zewT6aXn&v=1765791561" }
                });

            migrationBuilder.InsertData(
                table: "Reader",
                columns: new[] { "ReaderID", "Age", "Email", "FirstName", "IdentityUserId", "LastName", "NumberOfReadBooks", "ProfilePictureUrl" },
                values: new object[,]
                {
                    { 1, 18, "eva.valkova.1003@gmail.com", "Eva", null, "Valkova", 254, "https://media.licdn.com/dms/image/D4D03AQH1b0n1bX2m5g/profile-displayphoto-shrink_800_800/0/1683296144862?e=2147483647&v=beta&t=YlKXJ1d8YxY1G3E5KXc1Y3nU6kq5r0F1b4r3F4Z3K2o" },
                    { 2, 25, "eva.n.valkova@gmail.com", "Eva N.", null, "Valkova", 102, "https://media.licdn.com/dms/image/D4D03AQH1b0n1bX2m5g/profile-displayphoto-shrink_800_800/0/1683296144862?e=2147483647&v=beta&t=YlKXJ1d8YxY1G3E5KXc1Y3nU6kq5r0F1b4r3F4Z3K2o" }
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
                table: "BookListReader",
                columns: new[] { "BookListReaderID", "BookID", "BookRating", "BookReview", "ReaderID" },
                values: new object[,]
                {
                    { 1, 1, 4.5m, "An intense and gripping read that delves deep into the complexities of addiction and love. The characters are well-developed, and the chemistry between Lily and Loren is palpable. A must-read for fans of contemporary romance.", 1 },
                    { 2, 4, 5.0m, "A timeless classic that beautifully captures the nuances of love, social class, and personal growth. Elizabeth Bennet is a strong and relatable heroine, and Mr. Darcy's transformation is compelling. Austen's wit and keen observations make this novel a joy to read.", 1 },
                    { 3, 5, 4.8m, "A magical journey that captivates readers of all ages. J.K. Rowling's world-building is exceptional, and the characters are memorable and endearing. The story of friendship, bravery, and self-discovery is beautifully told. A fantastic start to an iconic series.", 1 },
                    { 4, 6, 4.3m, "A steamy and emotionally charged romance that explores the complexities of rivalry and attraction. The chemistry between Shane and Ilya is electric, and their journey towards understanding and acceptance is heartwarming. Rachel Reid delivers a compelling story that keeps readers hooked.", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_Email",
                table: "Author",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorID",
                table: "Book",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_BookListAuthor_AuthorID",
                table: "BookListAuthor",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_BookListAuthor_BookID",
                table: "BookListAuthor",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookListReader_BookID_ReaderID",
                table: "BookListReader",
                columns: new[] { "BookID", "ReaderID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookListReader_ReaderID",
                table: "BookListReader",
                column: "ReaderID");

            migrationBuilder.CreateIndex(
                name: "IX_Reader_Email",
                table: "Reader",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookListAuthor");

            migrationBuilder.DropTable(
                name: "BookListReader");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Reader");

            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
