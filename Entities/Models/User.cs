using Microsoft.AspNetCore.Identity;

namespace Entities.Models;
public class User : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public ICollection<UserHistory> History { get; set; }
}

