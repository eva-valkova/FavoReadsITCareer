using FavoReads.Data;
using FavoReads.DTOs;
using FavoReads.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FavoReads.Controllers
{
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
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto == null) return BadRequest("No Data.");

            var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            try
            {

                var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);
                if (!roleResult.Succeeded)
                {
                    return BadRequest("The Role cannot be added.");
                }

                if (dto.Role == "Author")
                {
                    _context.Author.Add(new Author
                    {
                        FirstName = dto.FirstName ?? "Name",
                        LastName = dto.LastName ?? "Last Name",
                        Email = dto.Email,
                        IdentityUserId = user.Id,
                        Biography = "No Bio.",
                        Age = dto.Age,
                        ProfilePictureUrl = ""
                    });
                }
                else if (dto.Role == "Reader")
                {
                    _context.Reader.Add(new Reader
                    {
                        FirstName = dto.FirstName ?? "Name",
                        LastName = dto.LastName ?? "Last Name",
                        Email = dto.Email,
                        IdentityUserId = user.Id,
                        Age = dto.Age,
                        ProfilePictureUrl = ""
                    });
                }

                await _context.SaveChangesAsync();

                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(new { message = "User created successfully" });
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(user);
                return StatusCode(500, $"Error when saving: {ex.Message}");
            }
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

            //return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}