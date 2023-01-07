using Ukrainian_Culture.Tests.RepostoriesTests.DbModels;
using Ukrainian_Culture.Tests.RepostoryTests.DbModels;

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