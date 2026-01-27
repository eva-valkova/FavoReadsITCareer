using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ReaderController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReaderController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public IActionResult Create(CreateReaderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        bool emailExists = _context.Reader.Any(r => r.Email == dto.Email);

        if (emailExists)
            return Conflict("Email already exists");

        var reader = new Reader
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Age = dto.Age,
            NumberOfReadBooks = 0
        };

        _context.Reader.Add(reader);
        _context.SaveChanges();

        return Ok(reader);
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var readers = _context.Reader.ToList();
        return Ok(readers);
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var reader = _context.Reader.Find(id);

        if (reader == null)
            return NotFound("Reader not found");

        return Ok(reader);
    }
    [HttpPut]
    public IActionResult Update(UpdateReaderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reader = _context.Reader.Find(dto.ReaderId);

        if (reader == null)
            return NotFound("Reader not found");

        reader.FirstName = dto.FirstName;
        reader.LastName = dto.LastName;
        reader.Age = dto.Age;

        _context.SaveChanges();

        return Ok(reader);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reader = _context.Reader
            .Include(r => r.BookListReaders)
            .FirstOrDefault(r => r.ReaderID == id);

        if (reader == null)
            return NotFound("Reader not found");

        _context.BookListReader.RemoveRange(reader.BookListReaders);
        _context.Reader.Remove(reader);

        _context.SaveChanges();

        return NoContent();
    }
}