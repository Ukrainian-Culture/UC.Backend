using Microsoft.EntityFrameworkCore.Metadata;

namespace Ukrainian_Culture.Tests.DbModels;

public interface ITestableModel
{
    IModel GetModel();
}