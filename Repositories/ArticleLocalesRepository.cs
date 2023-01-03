using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class ArticleLocalesRepository : RepositoryBase<ArticlesLocale>, IArticleLocalesRepository
{
    public ArticleLocalesRepository(RepositoryContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<ArticlesLocale>> GetTileDataOfArticlesAsync(int cultureId, ChangesType asNoTracking)
    {
        var data
            =  await Context
                .ArticlesLocales
                .AsNoTracking()
                .Where(artLocale => artLocale.CultureId == cultureId)
                .Select(artLocale => new ArticlesLocale
                {
                    SubText = artLocale.SubText,
                    ShortDescription = artLocale.ShortDescription,
                    Title = artLocale.Title
                })
                .ToListAsync();

        return data;
    }
}