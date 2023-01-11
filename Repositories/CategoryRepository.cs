using Contracts;
using Entities;
using Entities.Models;

namespace Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext context)
        : base(context)
    {
    }
}