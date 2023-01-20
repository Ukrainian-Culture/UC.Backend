using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class SignInUser
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
