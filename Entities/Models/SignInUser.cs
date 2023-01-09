using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class SignInUser
{
    [Required]
    public string FirstName { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
