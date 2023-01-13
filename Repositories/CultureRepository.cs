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

    public async Task<Culture> GetCultureWithContentAsync(Guid cultureId, ChangesType asNoTracking)
    => await Context.Cultures.AsNoTracking()
            .Include(cult => cult.ArticlesTranslates)
            .Include(cult => cult.Categories)
            .FirstAsync(cult => cult.Id == cultureId);

    public async Task<Culture?> GetCultureAsync(Guid cultureId, ChangesType asNoTracking)
        => await FindByCondition(cult => cult.Id == cultureId, asNoTracking).FirstOrDefaultAsync();
}