using FavoReads.Models;
using Microsoft.EntityFrameworkCore;
public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _context.Book.Include(b => b.Author).ToListAsync();
    }
}
