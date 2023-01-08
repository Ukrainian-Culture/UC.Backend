using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Ukrainian_Culture.Tests.DbModels;

public class UserModel : ITestableModel
{
    public IModel GetModel()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<User>().Property(user => user.FirstName).IsRequired();
        modelBuilder.Entity<User>().Property(user => user.LastName).IsRequired();
        modelBuilder.Entity<User>().Ignore(p => p.AccessFailedCount);
        modelBuilder.Entity<User>().Ignore(p => p.ConcurrencyStamp);
        modelBuilder.Entity<User>().Ignore(p => p.Email);
        modelBuilder.Entity<User>().Ignore(p => p.EmailConfirmed);
        modelBuilder.Entity<User>().Ignore(p => p.LockoutEnabled);
        modelBuilder.Entity<User>().Ignore(p => p.LockoutEnd);
        modelBuilder.Entity<User>().Ignore(p => p.SecurityStamp);
        modelBuilder.Entity<User>().Ignore(p => p.PhoneNumberConfirmed);
        modelBuilder.Entity<User>().Ignore(p => p.PhoneNumber);
        modelBuilder.Entity<User>().Ignore(p => p.PasswordHash);
        modelBuilder.Entity<User>().Ignore(p => p.NormalizedUserName);
        modelBuilder.Entity<User>().Ignore(p => p.NormalizedEmail);
        modelBuilder.Entity<User>().Ignore(p => p.UserName);
        modelBuilder.Entity<User>().Ignore(p => p.TwoFactorEnabled);

        return modelBuilder.FinalizeModel();
    }
}