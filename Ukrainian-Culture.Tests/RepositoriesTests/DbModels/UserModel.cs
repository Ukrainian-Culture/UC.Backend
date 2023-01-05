namespace Ukrainian_Culture.Tests.RepositoriesTests.DbModels;

public class UserModel : ITestableModel
{
    public IModel GetModel()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<User>().Property(user => user.Login).IsRequired();
        modelBuilder.Entity<User>().Property(user => user.Name).IsRequired();
        modelBuilder.Entity<User>().Property(user => user.Phone).IsRequired();

        return modelBuilder.FinalizeModel();
    }
}