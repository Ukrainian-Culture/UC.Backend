using Contracts;
using Entities;
using Entities.Models;

namespace Repositories;

public class CategoryLocalesRepository : RepositoryBase<CategoryLocale>, ICategoryLocalesRepository
{
    public CategoryLocalesRepository(RepositoryContext context)
        : base(context)
    {
    }
}