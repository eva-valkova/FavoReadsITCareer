using Microsoft.AspNetCore.Mvc;          
using Microsoft.AspNetCore.Authorization; 
using Microsoft.EntityFrameworkCore;    
using System.Security.Claims;           
using FavoReads.DTOs;
using FavoReads.Models;

namespace FavoReads.Controllers
{
    [Authorize(Roles = "Author")]
public class AuthorController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var author = _context.Author.FirstOrDefault(a => a.IdentityUserId == userId);

        if (author == null) return NotFound();

        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(UpdateAuthorDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var author = _context.Author.FirstOrDefault(a => a.IdentityUserId == userId);

        if (author == null) return Unauthorized();

        author.FirstName = dto.FirstName;
        author.LastName = dto.LastName;
        author.Biography = dto.Biography;

        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
}