using Microsoft.EntityFrameworkCore;
using FavoReads.Controllers;
using FavoReads.DTOs;
namespace FavoReads.Services
{
    public class BookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        //  Get books only for this author
        public async Task<List<Book>> SearchBooks(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return await _context.Book
                    .Include(b => b.Author)
                   .ToListAsync();

            searchTerm = searchTerm.ToLower();

            return await _context.Book
                .Include(b => b.Author)
                .Where(b =>
                    b.Title.ToLower().Contains(searchTerm) ||
                    b.Author.LastName.ToLower().Contains(searchTerm))
                .ToListAsync();
        }
        public List<Book> GetBooksByAuthor(string identityUserId)
        {
            return _context.Book
                .Where(b => b.Author.IdentityUserId == identityUserId)
                .ToList();
        }

        // Add book ONLY for logged author
        public async Task AddBook(CreateBookDto dto, string identityUserId)
        {
            var author = _context.Author
                .FirstOrDefault(a => a.IdentityUserId == identityUserId);

            if (author == null)
                throw new Exception("Author not found");

            var book = new Book
            {
                Title = dto.Title,
                AuthorID = author.AuthorID
            };

            _context.Book.Add(book);
            await _context.SaveChangesAsync();
        }

        // Security check on delete
        public async Task DeleteBook(int id, string identityUserId)
        {
            var book = _context.Book
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookID == id);

            if (book.Author.IdentityUserId != identityUserId)
                throw new Exception("NOT YOUR BOOK");

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<double> GetAverageRating(int bookId)
        {
            return (double)await _context.BookListReader
                .Where(r => r.BookID == bookId)
                .Select(r => r.BookRating)
                .DefaultIfEmpty(0)
                .AverageAsync();
        }

       

    }
}