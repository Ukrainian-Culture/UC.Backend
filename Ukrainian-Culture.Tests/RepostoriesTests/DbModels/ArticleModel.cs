using Ukrainian_Culture.Tests.RepostoriesTests.DbModels;
using Ukrainian_Culture.Tests.RepostoryTests.DbModels;

public class ArticleModel : ITestableModel

{

    public IModel GetModel()

    {
        var modelBuilder = new ModelBuilder();

        modelBuilder.CreateArtilesModel();
        modelBuilder.CreateCategoryModel();

        return modelBuilder.FinalizeModel();
    }
}