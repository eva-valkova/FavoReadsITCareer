using FavoReads.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FavoReads.DTOs;
using FavoReads.Models;

namespace FavoReads.Controllers
{
    [Authorize(Roles = "Author")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly FavoReads.Services.BookService _bookService;
        private readonly FavoReads.Services.ReviewService _reviewService;

        public BookController(FavoReads.Services.BookService bookService,
                              FavoReads.Services.ReviewService reviewService,
                              ApplicationDbContext context)
        {
            _bookService = bookService;
            _reviewService = reviewService;
            _context = context;
        }

        
        [HttpGet]
        [AllowAnonymous] 
        public IActionResult Index()
        {
            var books = _context.Book
                .Include(b => b.Author)
                .ToList();

            return View(books);
        }

        [HttpGet]
        public IActionResult MyBooks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var books = _context.Book
                .Where(b => b.Author.IdentityUserId == userId)
                .ToList();

            return View(books);
        }

        // 3. CREATE BOOK (Show the Form)
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Returns the Create.cshtml view
        }

        // 4. CREATE BOOK (Process the Submission)
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
                AuthorID = author.AuthorID
            };

            _context.Book.Add(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(MyBooks));
        }

        // 5. EDIT BOOK (Show the Form with existing data)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = _context.Book
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookID == id);

            if (book == null) return NotFound();
            if (book.Author.IdentityUserId != userId) return Forbid();

            var dto = new UpdateBookDto { Title = book.Title };
            return View(dto);
        }

        // 6. EDIT BOOK (Process the Update)
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

            book.Title = dto.Title;
            _context.SaveChanges();

            return RedirectToAction(nameof(MyBooks));
        }

        // 7. DELETE BOOK
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

        // 8. BOOK DETAILS
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

        // 9. SEARCH
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