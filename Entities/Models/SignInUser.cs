using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class SignInUser
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
