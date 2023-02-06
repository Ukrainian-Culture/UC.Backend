using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class ChangeLastNameDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string NewLastName { get; set; } = null!;
}
