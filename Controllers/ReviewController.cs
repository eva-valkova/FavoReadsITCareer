using FavoReads.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FavoReads.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult AddReview(CreateReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool exists = _context.BookListReader.Any(r =>
                r.BookID == dto.BookId &&
                r.ReaderID == dto.ReaderId);

            if (exists)
                return Conflict("Reader already reviewed this book");

            var review = new BookListReader
            {
                BookID = dto.BookId,
                ReaderID = dto.ReaderId,
                BookRating = dto.Rating,
                BookReview = dto.Review
            };

            _context.BookListReader.Add(review);
            _context.SaveChanges();

            return Ok(review);
        }

        [HttpPut]
        public IActionResult UpdateReview(UpdateReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = _context.BookListReader.Find(dto.ReviewId);

            if (review == null)
                return NotFound("Review not found");

            review.BookRating = dto.Rating;
            review.BookReview = dto.Review;

            _context.SaveChanges();

            return Ok(review);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var review = _context.BookListReader.Find(id);

            if (review == null)
                return NotFound("Review not found");

            _context.BookListReader.Remove(review);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpGet("{bookId}/ratings")]
        public IActionResult GetBookRatings(int bookId)
        {
            var reviews = _context.BookListReader
                .Where(r => r.BookID == bookId);

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
