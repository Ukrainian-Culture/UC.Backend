using Contracts;
using Entities;
using Entities.Models;

namespace Repositories;

public class CategoryLocalesRepository : ICategoryLocalesRepository// RepositoryBase<CategoryLocale>,
{
    public CategoryLocalesRepository(RepositoryContext context)
      //  : base(context)
    {
    }
}