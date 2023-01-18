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

    public async Task<CategoryLocale?> GetFirstByConditionAsync(Expression<Func<CategoryLocale, bool>> expression,
        ChangesType changesType)
        => await FindByCondition(expression, changesType).FirstOrDefaultAsync();

    public void CreateCategoryLocaleForCulture(Guid cultureId, CategoryLocale categoryEntity)
    {
        categoryEntity.CultureId = cultureId;
        categoryEntity.CategoryId = Guid.NewGuid();
        Create(categoryEntity);
    }

    public void DeleteCategoryLocale(CategoryLocale categoryLocale) => Delete(categoryLocale);
}