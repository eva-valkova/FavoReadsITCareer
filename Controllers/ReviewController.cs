using FavoReads.Data;
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
            var reader = _context.Reader.FirstOrDefault(r => r.IdentityUserId == userId);

            if (reader == null) return Unauthorized();

            // Проверка дали вече има запис (четене или ревю)
            var existingRecord = _context.BookListReader
                .FirstOrDefault(r => r.BookID == dto.BookId && r.ReaderID == reader.ReaderID);

            if (existingRecord != null && !string.IsNullOrEmpty(existingRecord.BookReview))
            {
                // Ако вече има ревю, можеш да пренасочиш с грешка или просто да не правиш нищо
                return RedirectToAction("Details", "Book", new { id = dto.BookId });
            }

            if (existingRecord != null)
            {
                // Ако е имало само запис "искам да прочета", просто го обновяваме с ревю
                existingRecord.BookRating = dto.Rating;
                existingRecord.BookReview = dto.Review;
            }
            else
            {
                // Ако е напълно нов запис
                var review = new BookListReader
                {
                    BookID = dto.BookId,
                    ReaderID = reader.ReaderID,
                    BookRating = dto.Rating,
                    BookReview = dto.Review
                };
                _context.BookListReader.Add(review);
            }

            _context.SaveChanges();

            // Пренасочваме обратно към детайлите на книгата, за да се види новото ревю
            return RedirectToAction("Details", "Book", new { id = dto.BookId });
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