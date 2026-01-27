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

    [HttpPost]
    public IActionResult Create(CreateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var author = _context.Author.Find(dto.AuthorId);

        if (author == null)
            return NotFound("Author not found");

        var book = new Book
        {
            Title = dto.Title,
            AuthorID = dto.AuthorId,
            Genre = dto.Genre,
            Description = dto.Description,
            CoverImageUrl = dto.CoverImageUrl,
            AverageRating = dto.AverageRating
        };

        _context.Book.Add(book);
        _context.SaveChanges();

        return Ok(book);
    }
    [HttpPut]
    public IActionResult Update(UpdateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var book = _context.Book.Find(dto.BookId);

        if (book == null)
            return NotFound("Book not found");

        var authorExists = _context.Author.Any(a => a.AuthorID == dto.AuthorId);

        if (!authorExists)
            return NotFound("Author not found");

        book.Title = dto.Title;
        book.AuthorID = dto.AuthorId;
        book.Genre = dto.Genre;
        book.Description = dto.Description;
        book.CoverImageUrl = dto.CoverImageUrl;
        book.AverageRating = dto.AverageRating;

        _context.SaveChanges();

        return Ok(book);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var book = _context.Book
            .Include(b => b.BookListReader)
            .Include(b => b.BookListAuthor)
            .FirstOrDefault(b => b.BookID == id);

        if (book == null)
            return NotFound("Book not found");


        _context.BookListReader.RemoveRange(book.BookListReader);
        _context.BookListAuthor.RemoveRange(book.BookListAuthor);

        _context.Book.Remove(book);
        _context.SaveChanges();

        return NoContent();
    }

}
