using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class SendEmailDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Url { get; set; }
    }
}
