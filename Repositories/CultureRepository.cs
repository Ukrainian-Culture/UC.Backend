using System.Linq.Expressions;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class CultureRepository : RepositoryBase<Culture>, ICultureRepository
{
    public CultureRepository(RepositoryContext context)
        : base(context)
    {
    }

    public async Task<Culture?> GetFirstWithIncludesAsync(Expression<Func<Culture, bool>> func,
        ChangesType asNoTracking)
        => await Context.Cultures.AsNoTracking()
            .Include(cult => cult.ArticlesTranslates)
            .FirstAsync(func);

    public async Task<Culture?> GetFirstByConditionAsync(Expression<Func<Culture, bool>> func, ChangesType changesType)
        => await FindByCondition(func, changesType).FirstOrDefaultAsync();

    public async Task<IEnumerable<Culture>> GetAllCulturesByCondition(Expression<Func<Culture, bool>> func,
        ChangesType asNoTracking)
        => await FindByCondition(func, asNoTracking).ToListAsync();


    public void CreateCulture(Culture cultureEntity)
    {
        cultureEntity.Id = Guid.NewGuid();
        Create(cultureEntity);
    }

    public void DeleteCulture(Culture culture) => Delete(culture);
}