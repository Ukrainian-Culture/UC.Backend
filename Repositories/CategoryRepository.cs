using Contracts;
using Entities;
using Entities.Models;

namespace Repositories;

public class CategoryRepository : ICategoryRepository//RepositoryBase<Category>,
{
    public CategoryRepository(RepositoryContext context)
        //: base(context)
    {
    }
}