using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public int Age { get; set; }  
}

