using FavoReads.Models;
using Microsoft.AspNetCore.Mvc;

public class BooksController : Controller
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllBooksAsync();
        return View(books);
    }
}
