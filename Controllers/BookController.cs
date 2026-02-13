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
        private ApplicationDbContext _context;
        private FavoReads.Services.BookService _bookService;

        private readonly FavoReads.Services.ReviewService _reviewService;

        public BookController(FavoReads.Services.BookService bookService,
                              FavoReads.Services.ReviewService reviewService,
                              ApplicationDbContext context)
        {
            _bookService = bookService;
            _reviewService = reviewService;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var author = _context.Author
                .FirstOrDefault(a => a.IdentityUserId == userId);

            if (author == null)
                return Unauthorized();

            var book = new Book
            {
                Title = dto.Title,
                AuthorID = author.AuthorID   // 🔒 forced ownership
            };

            _context.Book.Add(book);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPost]
        public IActionResult Edit(int id, UpdateBookDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var book = _context.Book
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookID == id);

            if (book == null)
                return NotFound();

            if (book.Author.IdentityUserId != userId)
                return Forbid();

            book.Title = dto.Title;

            _context.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var book = _context.Book
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookID == id);

            if (book == null)
                return NotFound();

            if (book.Author.IdentityUserId != userId)
                return Forbid();

            _context.Book.Remove(book);
            _context.SaveChanges();

            return Ok();
        }

        public IActionResult MyBooks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var books = _context.Book
                .Where(b => b.Author.IdentityUserId == userId)
                .ToList();

            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Book
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null)
                return NotFound();

            var averageRating = await _bookService.GetAverageRating(id);
            var reviews = await _reviewService.GetReviewsForBook(id);

            ViewBag.AverageRating = averageRating;
            ViewBag.Reviews = reviews;

            return View(book);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Search(string query)
        {
            var books = await _bookService.SearchBooks(query);

            ViewBag.SearchTerm = query;

            return View("Index", books);
        }

    }
}