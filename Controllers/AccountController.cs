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

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            // 1. DO YOUR CUSTOM TABLES FIRST
            var roleNormalized = dto.Role?.ToUpper().Trim();
            if (roleNormalized == "AUTHOR")
            {
                _context.Author.Add(new Author { Email = dto.Email, IdentityUserId = user.Id });
            }
            else if (roleNormalized == "READER")
            {
                _context.Reader.Add(new Reader { Email = dto.Email, IdentityUserId = user.Id });
            }

            await _context.SaveChangesAsync(); // Save to your tables first

            // 2. DO THE ROLE LAST
            try
            {
                await _userManager.AddToRoleAsync(user, dto.Role);
            }
            catch
            {
                /* Log error but at least the user and author are saved */
            }

            return Ok();
        }

        return Ok("User created");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return Unauthorized();

        var result = await _signInManager.PasswordSignInAsync(
            user,
            dto.Password,
            isPersistent: false,
            lockoutOnFailure: false
        );

        if (!result.Succeeded)
            return Unauthorized();

        return RedirectToAction("Index", "Home"); 
        
        return Ok();
    }



    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out");
    }


}
