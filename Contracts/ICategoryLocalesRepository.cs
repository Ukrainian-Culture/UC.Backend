using System.Linq.Expressions;
using Entities.Models;

namespace Contracts;

public interface ICategoryLocalesRepository
{
    Task<IEnumerable<CategoryLocale>> GetAllByConditionAsync(Expression<Func<CategoryLocale, bool>> expression,
        ChangesType changesType);
}