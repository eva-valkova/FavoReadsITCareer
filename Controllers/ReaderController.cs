using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FavoReads.DTOs;
using FavoReads.Models;
using FavoReads.Data;

[Authorize(Roles = "Reader")] // Само читатели имат достъп до този контролер
public class ReaderController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReaderController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. ЛИЧЕН ПРОФИЛ (Вместо Index, който показва всички)
    [HttpGet]
    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reader = _context.Reader.FirstOrDefault(r => r.IdentityUserId == userId);

        if (reader == null) return NotFound("Профилът не е намерен.");

        return View(reader); // Връща Index.cshtml с данните на логнатия читател
    }

    // 2. МОИТЕ КНИГИ (Вече не приемаме readerId от URL, а го намираме сами)
    [HttpGet("my-books")]
    public async Task<IActionResult> MyBooks()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reader = await _context.Reader.FirstOrDefaultAsync(r => r.IdentityUserId == userId);

        if (reader == null) return Unauthorized();

        var myBooks = await _context.BookListReader
            .Where(br => br.ReaderID == reader.ReaderID)
            .Include(br => br.Book) 
            .ThenInclude(b => b.Author) 
            .ToListAsync();

        return View(myBooks); 
    }

    // 3. ДЕТАЙЛИ (Защитен метод)
    [HttpGet("Details")]
    public IActionResult GetDetails()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reader = _context.Reader.FirstOrDefault(r => r.IdentityUserId == userId);

        if (reader == null) return NotFound();
        
        return View(reader); // Или връща Ok(reader) за API
    }

    // 4. ОБНОВЯВАНЕ (Само на собствения профил)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(UpdateReaderDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reader = _context.Reader.FirstOrDefault(r => r.IdentityUserId == userId);

        if (reader == null) return Unauthorized();

        if (!ModelState.IsValid) return View(dto);

        reader.FirstName = dto.FirstName;
        reader.LastName = dto.LastName;
        reader.Age = dto.Age;

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    // 5. ИЗТРИВАНЕ (Читателят трие собствения си профил)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteAccount()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reader = _context.Reader
            .Include(r => r.BookListReaders)
            .FirstOrDefault(r => r.IdentityUserId == userId);

        if (reader == null) return NotFound();

        // Изтриваме ревютата му и самия профил
        _context.BookListReader.RemoveRange(reader.BookListReaders);
        _context.Reader.Remove(reader);
        _context.SaveChanges();

        // Тук трябва да се добави и излизане (Logout) след изтриване
        return RedirectToAction("Index", "Home");
    }

    // Този метод обикновено се вика автоматично при Register, 
    // затова е добре да е защитен или да се премести в AccountController
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Create(CreateReaderDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        bool emailExists = _context.Reader.Any(r => r.Email == dto.Email);
        if (emailExists) return Conflict("Email already exists");

        var reader = new Reader
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Age = dto.Age,
            NumberOfReadBooks = 0,
            IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) // Свързваме го!
        };

        _context.Reader.Add(reader);
        _context.SaveChanges();

        return Ok(reader);
    }
}