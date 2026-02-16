using FavoReads.DTOs;
using FavoReads.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FavoReads.Controllers
{
    [Authorize(Roles = "Reader")] // Само читатели имат достъп до тези методи
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. ДОБАВЯНЕ НА РЕВЮ (Само 1 на книга)
        [HttpPost]
        public IActionResult AddReview(CreateReviewDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Намираме записа на читателя в базата, свързан с логнатия Identity потребител
            var reader = _context.Reader.FirstOrDefault(r => r.IdentityUserId == userId);

            if (reader == null) return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ПРОВЕРКА: Вече съществува ли ревю от ТОЗИ читател за ТАЗИ книга?
            bool exists = _context.BookListReader.Any(r =>
                r.BookID == dto.BookId &&
                r.ReaderID == reader.ReaderID);

            if (exists)
                return Conflict("Вече сте написали ревю за тази книга.");

            var review = new BookListReader
            {
                BookID = dto.BookId,
                ReaderID = reader.ReaderID, // Използваме реалното ID от базата, не само от DTO-то
                BookRating = dto.Rating,
                BookReview = dto.Review
            };

            _context.BookListReader.Add(review);
            _context.SaveChanges();

            return Ok(review);
        }

        // 2. РЕДАКЦИЯ (Само на собствено ревю)
        [HttpPut]
        public IActionResult UpdateReview(UpdateReviewDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reader = _context.Reader.FirstOrDefault(r => r.IdentityUserId == userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Търсим ревюто, но задължително проверяваме дали ReaderID съвпада с логнатия
            var review = _context.BookListReader
                .FirstOrDefault(r => r.BookListReaderID == dto.ReviewId && r.ReaderID == reader.ReaderID);

            if (review == null)
                return NotFound("Ревюто не е намерено или нямате право да го редактирате.");

            review.BookRating = dto.Rating;
            review.BookReview = dto.Review;

            _context.SaveChanges();

            return Ok(review);
        }

        // 3. ИЗТРИВАНЕ (Само на собствено ревю)
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reader = _context.Reader.FirstOrDefault(r => r.IdentityUserId == userId);

            var review = _context.BookListReader
                .FirstOrDefault(r => r.BookListReaderID == id && r.ReaderID == reader.ReaderID);

            if (review == null)
                return NotFound("Ревюто не е намерено или нямате право да го изтриете.");

            _context.BookListReader.Remove(review);
            _context.SaveChanges();

            return NoContent();
        }

        // 4. ПОЛУЧАВАНЕ НА РЕЙТИНГ (Публично достъпно за четене)
        [HttpGet("{bookId}/ratings")]
        [AllowAnonymous] // Всеки може да вижда средния рейтинг на книгата
        public IActionResult GetBookRatings(int bookId)
        {
            var reviews = _context.BookListReader.Where(r => r.BookID == bookId);

            if (!reviews.Any())
                return Ok(new AverageBookRatingDTO
                {
                    BookId = bookId,
                    AverageRating = 0,
                    ReviewCount = 0
                });

            return Ok(new AverageBookRatingDTO
            {
                BookId = bookId,
                AverageRating = (double)Math.Round(reviews.Average(r => r.BookRating), 2),
                ReviewCount = reviews.Count()
            });
        }
    }
}