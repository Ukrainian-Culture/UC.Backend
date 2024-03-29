﻿namespace Ukrainian_Culture.Tests.RepositoriesTests.DbModels;

public class CultureModel : ITestableModel
{
    public IModel GetModel()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.CreateArticlesLocaleModel();
        modelBuilder.CreateCultureModel();
        return modelBuilder.FinalizeModel();
    }
}