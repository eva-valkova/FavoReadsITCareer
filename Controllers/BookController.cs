using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context.Book
            .Include(b => b.Author)
            .ToList();

        return View(books);
    }
}
