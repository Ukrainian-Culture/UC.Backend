using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class ChangeFirstNameDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string NewFirstName { get; set; } = null!;
}
