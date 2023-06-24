using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetAllByConditionAsync(Expression<Func<Category, bool>> expression, ChangesType trackChanges)
        => await FindByCondition(expression, trackChanges).ToListAsync();

    public async Task<Category?> GetFirstByConditionAsync(Expression<Func<Category, bool>> expression, ChangesType trackChanges)
        => await FindByCondition(expression, trackChanges).FirstOrDefaultAsync();
}