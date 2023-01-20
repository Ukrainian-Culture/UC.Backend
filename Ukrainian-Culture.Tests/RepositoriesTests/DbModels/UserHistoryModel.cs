namespace Ukrainian_Culture.Tests.RepositoriesTests.DbModels;

public class UserHistoryModel : ITestableModel
{
    public IModel GetModel()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.CreateUserModel();
        modelBuilder.CreateUserHistoryModel();
        return modelBuilder.FinalizeModel();
    }

}