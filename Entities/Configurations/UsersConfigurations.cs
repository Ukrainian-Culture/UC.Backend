using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class UsersConfigurations : IEntityTypeConfiguration<User>
{
    private readonly Guid _firstId = new("169a9df2-231c-45e8-9a0a-c7333f0dc9f4");
    private readonly Guid _secondId = new("87d76511-8b74-4250-aef1-c47b8cb9308f");

    public void Configure(EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();
        builder.HasData(
            new User
            {
                Id = _firstId,
                UserName = "Admin",
                Email = "Admin@gmail.com",
                EmailConfirmed= true,
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "AdminPassword"),
                SecurityStamp = Guid.NewGuid().ToString(),
                SubscriptionEndDate = new DateTime(2023, 03, 19)
            },
            new User
            {
                Id = _secondId,
                UserName = "Bohdan",
                EmailConfirmed=true,
                NormalizedUserName = "BOHDAN",
                NormalizedEmail = "BOHDAN@GMAIL.COM",
                Email = "Bohdan@gmail.com",
                PasswordHash = hasher.HashPassword(null, "BohdanPassword"),
                SecurityStamp = Guid.NewGuid().ToString(),
                SubscriptionEndDate = new DateTime(2023, 05, 19)

            }
        );
    }
}