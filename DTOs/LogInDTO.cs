using System.ComponentModel.DataAnnotations;
namespace FavoReads.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}