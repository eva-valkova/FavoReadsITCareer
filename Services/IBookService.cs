using FavoReads.Models;

namespace FavoReads.Services
{
    public interface IBookService
    {
        Task<double> GetAverageRating(int bookId);
        Task<List<Book>> SearchBooks(string query);
        // Добави тук и другите методи, които си дефинирала в BookService
        public List<Book> GetBooksByAuthor(string identityUserId);
        //public Task AddReview(int bookId, string identityUserId, int rating, string reviewText);
        public Task AddBook(CreateBookDto dto, string identityUserId);
        public Task DeleteBook(int id, string identityUserId);
    }
}