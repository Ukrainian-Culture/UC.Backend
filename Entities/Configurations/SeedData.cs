using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configurations
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            SeedAdminRoles(builder);
            SeedRoles(builder);
        }
        private static void SeedAdminRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
           new IdentityUserRole<Guid>
           {
               RoleId = new("431f29e9-13ff-4f5f-b178-511610d5103f"),
               UserId = new("169a9df2-231c-45e8-9a0a-c7333f0dc9f4")
           }
          );
        }
        private static void SeedRoles(ModelBuilder builder)
        {
            var firstId = new Guid("431f29e9-13ff-4f5f-b178-511610d5103f");
            var SecondId = new Guid("5adbec33-97c5-4041-be6a-e0f3d3ca6f44");
            builder.Entity<Roles>().HasData(
                new Roles() { Id = firstId, Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new Roles() { Id = SecondId, Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }
    }
}
