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

        return modelBuilder.FinalizeModel();
    }
}