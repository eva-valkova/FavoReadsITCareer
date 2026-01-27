using System.ComponentModel.DataAnnotations;

public class UpdateReaderDto
{
    [Required]
    public int ReaderId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Range(1, 120)]
    public int Age { get; set; }
    public string ProfilePictureUrl { get; set; }
    [Required]
    public int NumberOfReadBooks { get; set; }
}
