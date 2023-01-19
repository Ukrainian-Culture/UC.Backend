using System.Linq.Expressions;
using Entities.Models;

namespace Contracts;

public interface ICategoryLocalesRepository
{
    Task<IEnumerable<CategoryLocale>> GetAllByConditionAsync(Expression<Func<CategoryLocale, bool>> expression,
        ChangesType changesType);

    Task<CategoryLocale?> GetFirstByConditionAsync(Expression<Func<CategoryLocale, bool>> expression,
        ChangesType changesType);
    void CreateCategoryLocaleForCulture(Guid cultureId, CategoryLocale categoryEntity);
    void DeleteCategoryLocale(CategoryLocale categoryLocale);
}