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

    public async Task<List<CategoryLocale>> GetCategoriesByCulture(int cultureId, ChangesType trackChanges)
    {
        return await Context.CategoryLocales.AsNoTracking()
            .Where(cat => cat.CultureId == cultureId)
            .Select(cat => new CategoryLocale
            {
                CategoryId = cat.CategoryId,
                Name = cat.Name
            }).ToListAsync();

    }
}