namespace Ukrainian_Culture.Tests.RepositoriesTests.DbModels;

public class CategoryLocaleModel : ITestableModel
{
    public IModel GetModel()

    {
        var modelBuilder = new ModelBuilder();

        modelBuilder.CreateCultureModel();
        modelBuilder.CreateArticlesLocaleModel();

        return modelBuilder.FinalizeModel();
    }
}