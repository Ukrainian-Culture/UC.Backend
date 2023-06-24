using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class ConfirmEmailDto
{
    public string Token { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
}
