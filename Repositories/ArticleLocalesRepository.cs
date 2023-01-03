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

    public async Task<ArticlesLocale> GetArticleLocaleByIdAsync(int id, ChangesType trackChanges)
       => await FindByCondition(art => art.Id == id, trackChanges)
           .SingleAsync();
}