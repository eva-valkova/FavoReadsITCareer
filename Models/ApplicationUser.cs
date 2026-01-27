using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public int ReaderId { get; set; }
    public required Reader Reader { get; set; }
}
