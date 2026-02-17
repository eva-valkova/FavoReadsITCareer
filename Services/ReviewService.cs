using FavoReads.Data;
using FavoReads.Models;
using Microsoft.EntityFrameworkCore;
namespace FavoReads.Services
{
    public class ReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        //  Add review (1 per book per reader)
        public async Task AddReview(int bookId, string identityUserId, int rating, string reviewText)
        {
            var reader = await _context.Reader
                .FirstOrDefaultAsync(r => r.IdentityUserId == identityUserId);

            if (reader == null)
                throw new Exception("Reader not found");

            //  Check if review already exists
            var existingReview = await _context.BookListReader
                .FirstOrDefaultAsync(r =>
                    r.BookID == bookId &&
                    r.ReaderID == reader.ReaderID);

            if (existingReview != null)
                throw new Exception("You already reviewed this book.");

            var review = new BookListReader
            {
                BookID = bookId,
                ReaderID = reader.ReaderID,
                BookRating = rating,
                BookReview = reviewText
            };

            _context.BookListReader.Add(review);
            await _context.SaveChangesAsync();
        }

        // Get average rating
        public async Task<double> GetAverageRating(int bookId)
        {
            var hasRatings = await _context.BookListReader
                .AnyAsync(r => r.BookID == bookId);

            if (!hasRatings)
            {
                return 0.00;
            }

            var average = await _context.BookListReader
                .Where(r => r.BookID == bookId)
                .AverageAsync(r => r.BookRating);

            return Math.Round(average, 2);
        }

        public async Task<List<BookListReader>> GetReviewsForBook(int bookId)
        {
            return await _context.BookListReader
                .Include(r => r.Reader)
                .Where(r => r.BookID == bookId)
                .ToListAsync();
        }

    }
}