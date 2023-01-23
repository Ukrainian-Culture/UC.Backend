using Microsoft.AspNetCore.Identity;

namespace Entities.Models;
public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<UserHistory> History { get; set; }
}

