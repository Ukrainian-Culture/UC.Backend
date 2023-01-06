namespace Ukrainian_Culture.Tests.RepositoriesTests.DbModels;

public class UserModel : ITestableModel
{
    public IModel GetModel()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.CreateUserModel();

        return modelBuilder.FinalizeModel();
    }
}