using FavoReads.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FavoReads.DTOs;
using FavoReads.Models;
using FavoReads.Data;

namespace FavoReads.Controllers
{
    // Само Автори могат да извършват действия в този контролер по подразбиране
    [Authorize(Roles = "Author")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookService _bookService;
        private readonly ReviewService _reviewService;

        public BookController(IBookService bookService, 
                              ReviewService reviewService, 
                              ApplicationDbContext context)
        {
            _bookService = bookService;
            _reviewService = reviewService;
            _context = context;
        }

        // 1. ПУБЛИЧЕН СПИСЪК (Всеки може да вижда всички книги)
        [HttpGet]
        [AllowAnonymous] 
        public IActionResult Index()
        {
            var books = _context.Book
                .Include(b => b.Author)
                .ToList();

            return View(books);
        }

        // 2. МОИТЕ КНИГИ (Само за логнатия автор)
        [HttpGet]
        public IActionResult MyBooks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Взимаме само книгите, чийто автор е логнатият потребител
            var books = _context.Book
                .Include(b => b.Author)
                .Where(b => b.Author.IdentityUserId == userId)
                .ToList();

            return View(books);
        }

        // 3. СЪЗДАВАНЕ НА КНИГА (Форма)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 4. СЪЗДАВАНЕ НА КНИГА (Процес)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateBookDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var author = _context.Author.FirstOrDefault(a => a.IdentityUserId == userId);

            if (author == null) return Unauthorized();

            if (!ModelState.IsValid) return View(dto);

            var book = new Book
            {
                Title = dto.Title,
                AuthorID = author.AuthorID // Автоматично свързваме с логнатия автор
            };

            _context.Book.Add(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(MyBooks));
        }

        // 5. РЕДАКЦИЯ (Форма - със защита)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = _context.Book
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookID == id);

            if (book == null) return NotFound();
            
            // ПРОВЕРКА: Ако книгата не е на този автор - ГРЕШКА
            if (book.Author.IdentityUserId != userId) return Forbid();

            var dto = new UpdateBookDto { Title = book.Title };
            return View(dto);
        }

        // 6. РЕДАКЦИЯ (Процес - със защита)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UpdateBookDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = _context.Book
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookID == id);

            if (book == null) return NotFound();
            if (book.Author.IdentityUserId != userId) return Forbid();

            if (!ModelState.IsValid) return View(dto);

            book.Title = dto.Title;
            _context.SaveChanges();

            return RedirectToAction(nameof(MyBooks));
        }

        // 7. ИЗТРИВАНЕ (Със защита)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = _context.Book
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookID == id);

            if (book == null) return NotFound();
            if (book.Author.IdentityUserId != userId) return Forbid();

            _context.Book.Remove(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(MyBooks));
        }

        // 8. ДЕТАЙЛИ (Публично)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Book
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null) return NotFound();

            ViewBag.AverageRating = await _bookService.GetAverageRating(id);
            ViewBag.Reviews = await _reviewService.GetReviewsForBook(id);

            return View(book);
        }

        // 9. ТЪРСЕНЕ (Публично)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string query)
        {
            var books = await _bookService.SearchBooks(query);
            ViewBag.SearchTerm = query;
            return View("Index", books);
        }
    }
}