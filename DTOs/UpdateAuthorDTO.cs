using System.ComponentModel.DataAnnotations;

public class UpdateAuthorDto
{
    [Required]
    public int AuthorId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Range(1, 120)]
    public int Age { get; set; }
    public string Biography { get; set; }
    public string ProfilePictureUrl { get; set; }

}
