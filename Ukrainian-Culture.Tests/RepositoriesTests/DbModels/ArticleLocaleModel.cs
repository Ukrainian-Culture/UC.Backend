namespace Ukrainian_Culture.Tests.RepositoriesTests.DbModels;

public class ArticlesLocaleModel : ITestableModel
{
    public IModel GetModel()

    {
        var modelBuilder = new ModelBuilder();

        modelBuilder.CreateArticlesLocaleModel();
        modelBuilder.CreateCultureModel();
        modelBuilder.CreateCategoriesLocaleModel();

        return modelBuilder.FinalizeModel();
    }
}