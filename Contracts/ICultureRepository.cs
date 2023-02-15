using System.Linq.Expressions;
using Entities.Models;

namespace Contracts;

public interface ICultureRepository
{
    Task<IEnumerable<Culture>> GetAllCulturesByCondition(Expression<Func<Culture, bool>> func, ChangesType changesType);
    Task<Culture?> GetFirstByConditionAsync(Expression<Func<Culture, bool>> func, ChangesType changesType);
    void CreateCulture(Culture cultureEntity);
    void DeleteCulture(Culture culture);
}