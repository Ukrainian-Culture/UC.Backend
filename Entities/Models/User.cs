using Microsoft.AspNetCore.Identity;

namespace Entities.Models;
public class User : IdentityUser<Guid>
{
    public ICollection<UserHistory> History { get; set; }
}

