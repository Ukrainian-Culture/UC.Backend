using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Entities.Configurations;

public static class SeedData
{
    public static void Seed(ModelBuilder builder)
    {
        SeedRoles(builder);
        SeedAdminRoles(builder);
    }
    private static void SeedAdminRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
       new IdentityUserRole<Guid>
       {
           RoleId = new("431f29e9-13ff-4f5f-b178-511610d5103f"),
           UserId = new("169a9df2-231c-45e8-9a0a-c7333f0dc9f4")
       },
       new IdentityUserRole<Guid>
       {
           RoleId = new("5adbec33-97c5-4041-be6a-e0f3d3ca6f44"),
           UserId = new("87d76511-8b74-4250-aef1-c47b8cb9308f")
       }
      );
    }
    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<Roles>().HasData(
            new Roles() { Id = new Guid("431f29e9-13ff-4f5f-b178-511610d5103f"), Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new Roles() { Id = new Guid("5adbec33-97c5-4041-be6a-e0f3d3ca6f44"), Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
            );
    }
}
