using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AuthorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthorsController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public IActionResult Create(CreateAuthorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool emailExists = _context.Author.Any(a => a.Email == dto.Email);

        if (emailExists)
            return Conflict("Email already exists");

        var author = new Author
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Age = dto.Age
        };

        _context.Author.Add(author);
        _context.SaveChanges();

        return Ok(author);
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Author.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var author = _context.Author.Find(id);

        if (author == null)
            return NotFound("Author not found");

        return Ok(author);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var author = _context.Author
            .Include(a => a.Books)
            .Include(a => a.BookListAuthors)
            .FirstOrDefault(a => a.AuthorID == id);

        if (author == null)
            return NotFound("Author not found");

        _context.BookListAuthor.RemoveRange(author.BookListAuthors);
        _context.Book.RemoveRange(author.Books);
        _context.Author.Remove(author);

        _context.SaveChanges();

        return NoContent();
    }
}