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

        [HttpGet]
        [AllowAnonymous] 
        public IActionResult Index()
        {
            var books = _context.Book.Include(b => b.Author).ToList();
            return View(books);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string query) 
        {
            // Ако заявката е празна, връщаме празен списък или всички книги
            var books = await _bookService.SearchBooks(query); 
            
            // Пълним ViewBag за Search.cshtml
            ViewBag.SearchQuery = query; 
            
            return View("Search", books); 
        }

        [HttpGet]
        public IActionResult MyBooks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var books = _context.Book
                .Include(b => b.Author)
                .Where(b => b.Author.IdentityUserId == userId)
                .ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult AddBook() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(CreateBookDto dto)
        {
            if (!ModelState.IsValid) return View("AddBook", dto);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var author = await _context.Author.FirstOrDefaultAsync(a => a.IdentityUserId == userId);

            var book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                CoverImageUrl = dto.CoverImageUrl,
                AuthorID = author.AuthorID
            };

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Book Added!";
            return RedirectToAction("Index", "Author");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            
            var book = await _context.Book.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookID == id);
            if (book == null) return NotFound();
            ViewBag.AverageRating = await _bookService.GetAverageRating(id);
            //ViewBag.Reviews = await _reviewService.GetReviewsForBook(id);
            ViewBag.Reviews = await _context.BookListReader
            .Include(r => r.Reader) // <--- ДОБАВИ ТОЗИ РЕД ТУК
            .Where(r => r.BookID == id) // Можеш да махнеш проверката за празно ревю, ако искаш да се брои за "прочетено" дори само с оценка
            .ToListAsync();
            return View(book);
        }

        
    }
}