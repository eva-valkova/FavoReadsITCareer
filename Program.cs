using FavoReads.Models;
using FavoReads.Services; // Увери се, че това е правилното име на папката ти със сървиси
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. РЕГИСТРАЦИЯ НА УСЛУГИ (Винаги ПРЕДИ builder.Build())
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Регистрираме сървисите тук:
// В Program.cs
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ReviewService>(); // Увери се, че ReviewService не използва интерфейс, иначе и там добави IReviewService

var app = builder.Build();

// 2. КОНФИГУРАЦИЯ НА PIPELINE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 3. СЪЗДАВАНЕ НА РОЛИ (Seed Roles)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Author", "Reader" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// 4. СТАРТИРАНЕ (Само веднъж!)
app.Run();