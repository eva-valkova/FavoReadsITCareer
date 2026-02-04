using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _context;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, dto.Role);

        // THIS PART CREATES DOMAIN USER
        if (dto.Role == "Author")
        {
            var author = new Author
            {
                Email = dto.Email,
                IdentityUserId = user.Id
            };

            _context.Author.Add(author);
        }
        else if (dto.Role == "Reader")
        {
            var reader = new Reader
            {
                Email = dto.Email,
                IdentityUserId = user.Id
            };

            _context.Reader.Add(reader);
        }

        await _context.SaveChangesAsync();

        return Ok("User created");
    }
}
