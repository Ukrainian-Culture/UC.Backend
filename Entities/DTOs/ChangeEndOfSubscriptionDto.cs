using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class ChangeEndOfSubscriptionDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public DateTime NewEndOfSubscription { get; set; }
}