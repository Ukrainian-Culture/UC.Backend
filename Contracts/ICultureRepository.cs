using System.Linq.Expressions;
using Entities.Models;

namespace Contracts;

public interface ICultureRepository
{
    Task<Culture> GetCultureWithContentAsync(Guid cultureId, ChangesType asNoTracking);
    Task<Culture?> GetCultureAsync(Guid cultureId, ChangesType asNoTracking);
    Task<IEnumerable<Culture>> GetCulturesByCondition(Expression<Func<Culture, bool>> func, ChangesType asNoTracking);
    void CreateCulture(Culture cultureEntity);
    void DeleteCulture(Culture culture);
}