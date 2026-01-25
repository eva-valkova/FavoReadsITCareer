namespace FavoReads.Models
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
    }
}
