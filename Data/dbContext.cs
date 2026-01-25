using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using FavoReads.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<Reader> Reader { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<BookListReader> BookListReader { get; set; }
    public DbSet<BookListAuthor> BookListAuthor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // -------- AUTHORS --------
        modelBuilder.Entity<Author>().HasData(
            new Author
            {
                AuthorID = 1,
                Email = "KBR@email.com",
                Password = "Lilo",
                FirstName = "Krista and Becca",
                LastName = "Ritchie",
                Age = 31,
                Biography = "Krista and Becca Ritchie are bestselling authors known for their contemporary romance novels. They have co-authored several popular series, including the 'Addicted' series and the 'Calloway Sisters' series. Their books often explore themes of love, friendship, and personal growth, and they have garnered a dedicated fanbase for their engaging storytelling and relatable characters.",
                ProfilePictureUrl = "https://static.wixstatic.com/media/bf6fdf_04623ef9a1f24586b3a082140c6e59e7~mv2.jpg/v1/crop/x_896,y_0,w_3209,h_3335/fill/w_253,h_263,al_c,q_80,usm_0.66_1.00_0.01,enc_avif,quality_auto/Krista%20and%20Becca%20-%20Author%20Photo%202.jpg"
            },
            new Author
            {
                AuthorID = 2,
                Email = "jane.austen@email.com",
                Password = "ElizabethDarcy",
                FirstName = "Jane",
                LastName = "Austen",
                Age = 41,
                Biography = "Jane Austen was an English novelist known primarily for her six major novels, which interpret, critique and comment upon the British landed gentry at the end of the 18th century. Austen's plots often explore",
                ProfilePictureUrl = "https://cdn.britannica.com/12/172012-050-DAA7CE6B/Jane-Austen-Cassandra-engraving-portrait-1810.jpg"
            },
            new Author
            {
                AuthorID = 3,
                Email = "jk.rowling@email.com",
                Password = "Romione",
                FirstName = "J.K.",
                LastName = "Rowling",
                Age = 58,
                Biography = "J.K. Rowling is a British author best known for writing the Harry Potter fantasy series, which has won multiple awards and sold more than 500 million copies worldwide, making it the best-selling book series in history. The books have been adapted into a popular film series, further expanding Rowling's influence in popular culture. Beyond Harry Potter, she has written novels for adults under the pseudonym Robert Galbraith.",
                ProfilePictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRq56NpoBJCRzrdQx2dftEAll-WVStdDCCAgw&s"
            },
            new Author
            {
                AuthorID = 4,
                Email = "rachel.reid@gmail.com",
                Password = "Hollanov",
                FirstName = "Rachel",
                LastName = "Reid",
                Age = 29,
                Biography = "Rachel Reid is a contemporary romance author known for her emotionally charged storytelling and complex characters. She has written several novels that explore themes of love, loss, and personal growth. Reid's work often features strong male hockey players",
                ProfilePictureUrl = "https://cdn.i-scmp.com/sites/default/files/styles/768x768/public/d8/images/canvas/2025/12/15/3d0392ca-0e60-4d31-a360-21da79d827f0_717ca59e.jpg?itok=zewT6aXn&v=1765791561"
            }
                );

        // -------- BOOKS --------
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                BookID = 1,
                Title = "Addicted To You",
                AuthorID = 1,
                Genre = "Contemporary Romance",
                Description = "Addicted to You is a contemporary romance novel by Krista and Becca Ritchie. It is the first book in the Addicted series and follows the story of Lily Calloway and Loren Hale as they navigate their complicated relationship, personal struggles, and the challenges of addiction.",
                CoverImageUrl = "https://cdn2.penguin.com.au/covers/original/9780593549476.jpg",
                AverageRating = 4.5m
            },
            new Book
            {
                BookID = 2,
                Title = "Ricochet",
                AuthorID = 1,
                Genre = "Contemporary Romance",
                Description = "Ricochet is the second book in the Addicted series by Krista and Becca Ritchie. The story continues to follow Lily Calloway and Loren Hale as they deal with the aftermath of the events in Addicted to You, facing new challenges in their relationship and personal lives.",
                CoverImageUrl = "https://m.media-amazon.com/images/I/81E1ZJyA-kL._UF1000,1000_QL80_.jpg",
                AverageRating = 4.4m
            },
            new Book
            {
                BookID = 3,
                Title = "Addicted For Now",
                AuthorID = 1,
                Genre = "Contemporary Romance",
                Description = "Addicted For Now is the third book in the Addicted series by Krista and Becca Ritchie. The story continues to follow Lily Calloway and Loren Hale as they navigate their complicated relationship, personal struggles, and the challenges of addiction.",
                CoverImageUrl = "https://images-eu.ssl-images-amazon.com/images/I/814gbvG+-RL._AC_UL600_SR600,600_.jpg",
                AverageRating = 4.6m
            },
            new Book
            {
                BookID = 4,
                Title = "Pride and Prejudice",
                AuthorID = 2,
                Genre = "Classic Romance",
                Description = "Pride and Prejudice is a classic novel by Jane Austen that explores themes of love, social class, and individual growth. The story follows Elizabeth Bennet as she navigates societal expectations and her evolving relationship with the proud Mr. Darcy.",
                CoverImageUrl = "https://bookoutlet.ca/api/image?url=https://images.bookoutlet.com/covers/large/isbn978059/9780593622452-l.jpg&w=3840&q=75",
                AverageRating = 4.8m
            },
            new Book
            {
                BookID = 5,
                Title = "Harry Potter and the Philosopher's Stone",
                AuthorID = 3,
                Genre = "Fantasy",
                Description = "Harry Potter and the Philosopher's Stone is the first book in the Harry Potter series by J.K. Rowling. It introduces readers to the magical world of Hogwarts and follows young wizard Harry Potter as he discovers his true heritage and begins his journey in the wizarding world.",
                CoverImageUrl = "https://upload.wikimedia.org/wikipedia/en/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg",
                AverageRating = 4.9m
            },
            new Book
            {
                BookID = 6,
                Title = "Heated Rivalry",
                AuthorID = 4,
                Genre = "Contemporary Romance",
                Description = "Heated Rivalry is a contemporary romance novel by Rachel Reid. The story follows the intense and passionate relationship between two rival hockey players - Shane Hollander and Ilya Rozanov who find themselves drawn to each other despite their competitive nature.",
                CoverImageUrl = "https://m.media-amazon.com/images/I/71iwUtZjamL._UF1000,1000_QL80_.jpg",
                AverageRating = 4.3m
            }
        );

        modelBuilder.Entity<Reader>().HasData(
            new Reader
            {
                ReaderID = 1,
                Email = "eva.valkova.1003@gmail.com",
                Password = "Coballoway",
                FirstName = "Eva",
                LastName = "Valkova",
                Age = 18,
                NumberOfReadBooks = 254,
                ProfilePictureUrl = "https://media.licdn.com/dms/image/D4D03AQH1b0n1bX2m5g/profile-displayphoto-shrink_800_800/0/1683296144862?e=2147483647&v=beta&t=YlKXJ1d8YxY1G3E5KXc1Y3nU6kq5r0F1b4r3F4Z3K2o"
            },
            new Reader
            {
                ReaderID = 2,
                Email = "eva.n.valkova@gmail.com",
                Password = "PrideAndPrejudice",
                FirstName = "Eva N.",
                LastName = "Valkova",
                Age = 25,
                NumberOfReadBooks = 102,
                ProfilePictureUrl = "https://media.licdn.com/dms/image/D4D03AQH1b0n1bX2m5g/profile-displayphoto-shrink_800_800/0/1683296144862?e=2147483647&v=beta&t=YlKXJ1d8YxY1G3E5KXc1Y3nU6kq5r0F1b4r3F4Z3K2o"
            }
            );

        modelBuilder.Entity<BookListReader>().HasData(
            new BookListReader
            {
                BookListReaderID = 1,
                BookID = 1,
                ReaderID = 1,
                BookRating = 4.5m,
                BookReview = "An intense and gripping read that delves deep into the complexities of addiction and love. The characters are well-developed, and the chemistry between Lily and Loren is palpable. A must-read for fans of contemporary romance."
            },
            new BookListReader
            {
                BookListReaderID = 1,
                BookID = 4,
                ReaderID = 1,
                BookRating = 5.0m,
                BookReview = "A timeless classic that beautifully captures the nuances of love, social class, and personal growth. Elizabeth Bennet is a strong and relatable heroine, and Mr. Darcy's transformation is compelling. Austen's wit and keen observations make this novel a joy to read."
            },
            new BookListReader
            {
                BookListReaderID = 1,
                BookID = 5,
                ReaderID = 1,
                BookRating = 4.8m,
                BookReview = "A magical journey that captivates readers of all ages. J.K. Rowling's world-building is exceptional, and the characters are memorable and endearing. The story of friendship, bravery, and self-discovery is beautifully told. A fantastic start to an iconic series."
            },
            new BookListReader
            {
                BookListReaderID = 1,
                BookID = 6,
                ReaderID = 1,
                BookRating = 4.3m,
                BookReview = "A steamy and emotionally charged romance that explores the complexities of rivalry and attraction. The chemistry between Shane and Ilya is electric, and their journey towards understanding and acceptance is heartwarming. Rachel Reid delivers a compelling story that keeps readers hooked."
            }
            );
        modelBuilder.Entity<BookListAuthor>().HasData(
            new BookListAuthor
            {
                BookListAuthorID = 1,
                BookID = 1,
                AuthorID = 1
            },
            new BookListAuthor
            {
                BookListAuthorID = 1,
                BookID = 2,
                AuthorID = 1
            },
            new BookListAuthor
            {
                BookListAuthorID = 1,
                BookID = 3,
                AuthorID = 1
            },
            new BookListAuthor
            {
                BookListAuthorID = 2,
                BookID = 4,
                AuthorID = 2
            },
            new BookListAuthor
            {
                BookListAuthorID = 3,
                BookID = 5,
                AuthorID = 3
            },
            new BookListAuthor
            {
                BookListAuthorID = 4,
                BookID = 6,
                AuthorID = 4
            }
            );
        modelBuilder.Entity<Reader>()
            .HasIndex(r => r.Email)
            .IsUnique();

        modelBuilder.Entity<Author>()
            .HasIndex(a => a.Email)
            .IsUnique();

        // BookRating decimal warning (optional)
        modelBuilder.Entity<BookListReader>()
            .Property(b => b.BookRating)
            .HasColumnType("decimal(3,2)"); // e.g., max 5.00

        modelBuilder.Entity<Book>()
            .Property(b => b.AverageRating)
            .HasColumnType("decimal(3,2)"); // e.g., max 5.00

        // Disable cascade delete on BookListAuthors to avoid multiple cascade paths
        modelBuilder.Entity<BookListAuthor>()
            .HasOne(bla => bla.Book)
            .WithMany(b => b.BookListAuthor)
            .HasForeignKey(bla => bla.BookID)
            .OnDelete(DeleteBehavior.Restrict); // NO CASCADE

        modelBuilder.Entity<BookListAuthor>()
            .HasOne(bla => bla.Author)
            .WithMany(a => a.BookListAuthors)
            .HasForeignKey(bla => bla.AuthorID)
            .OnDelete(DeleteBehavior.Restrict); // NO CASCADE

        // Optional: you can leave Books -> Authors cascade if you like
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorID)
            .OnDelete(DeleteBehavior.Cascade); // This one is fine
    }

}
