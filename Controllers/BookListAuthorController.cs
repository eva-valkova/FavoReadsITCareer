using Microsoft.AspNetCore.Mvc;

public class BookListAuthorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookListAuthorsController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public IActionResult Create(CreateBookListAuthorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool exists = _context.BookListAuthor.Any(x =>
            x.BookID == dto.BookId &&
            x.AuthorID == dto.AuthorId);

        if (exists)
            return Conflict("Relation already exists");

        var relation = new BookListAuthor
        {
            BookID = dto.BookId,
            AuthorID = dto.AuthorId
        };

        _context.BookListAuthor.Add(relation);
        _context.SaveChanges();

        return Ok(relation);
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.BookListAuthor.ToList());
    }
}