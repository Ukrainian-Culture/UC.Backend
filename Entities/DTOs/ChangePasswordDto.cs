using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class ChangePasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string CurrentPassword { get; set; } = null!;
    [Required]
    [Compare("ConfirmPassword")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "At least 8 symbols", MinimumLength = 8)]
    public string NewPassword { get; set; } = null!;
    [Required]
    public string ConfirmPassword { get; set; } = null!;
}
