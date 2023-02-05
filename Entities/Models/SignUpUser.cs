using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class SignUpUser
{

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Compare("ConfirmPassword")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "At least 8 symbols", MinimumLength = 8)]
    public string Password { get; set; } = null!;

    [Required]
    public string ConfirmPassword { get; set; } = null!;
}
