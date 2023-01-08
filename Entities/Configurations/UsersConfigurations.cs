using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class UsersConfigurations : IEntityTypeConfiguration<User>
{
    Guid firstId = new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4");
    Guid SecondId = new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f");
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = firstId,
                FirstName = "Vadym",
                LastName="Orlov",
                UserName="Vadym",
                NormalizedUserName="VADYM",
                NormalizedEmail="VADYM@GMAIL.COM",
                Email="Vadym@gmail.com",
                PasswordHash= "6925a4905d02cc4c26872e1713a0a5f2"

            },
            new User
            {
                Id =SecondId,
                FirstName = "Bohdan",
                LastName="Vivchar",
                UserName = "Bohdan",
                NormalizedUserName = "BOHDAN",
                NormalizedEmail = "BOHDAN@GMAIL.COM",
                Email = "Bohdan@gmail.com",
                PasswordHash = "6925a4905d02cc4c26872e1813a0a5f2"
            }
        );
    }
}