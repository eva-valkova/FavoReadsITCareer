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
