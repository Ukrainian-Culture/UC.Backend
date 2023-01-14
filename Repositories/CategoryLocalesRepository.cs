using System.Linq.Expressions;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class CategoryLocalesRepository : RepositoryBase<CategoryLocale>, ICategoryLocalesRepository
{
    public CategoryLocalesRepository(RepositoryContext context)
       : base(context)
    {
    }

    public async Task<IEnumerable<CategoryLocale>> GetAllByConditionAsync(Expression<Func<CategoryLocale, bool>> expression,
        ChangesType changesType)
        => await FindByCondition(expression, changesType).ToListAsync();

    public async Task<CategoryLocale?> GetFirstByCondition(Expression<Func<CategoryLocale, bool>> expression,
        ChangesType changesType)
        => await FindByCondition(expression, changesType).FirstOrDefaultAsync();
}