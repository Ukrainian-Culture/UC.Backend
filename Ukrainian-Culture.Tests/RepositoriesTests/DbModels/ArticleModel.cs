namespace Ukrainian_Culture.Tests.RepositoriesTests.DbModels;

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