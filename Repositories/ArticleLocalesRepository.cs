using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories;

public class ArticleLocalesRepository : RepositoryBase<ArticlesLocale>, IArticleLocalesRepository
{
    public ArticleLocalesRepository(RepositoryContext context)
        : base(context)
    {
    }

    public async Task<ArticlesLocale> GetArticlesLocaleByIdAsync(int id, ChangesType trackChanges)
       => await FindByCondition(art => art.Id == id, trackChanges)
           .SingleAsync();

    public async Task<IEnumerable<ArticlesLocale>> GetArticlesLocaleByCondition(Expression<Func<ArticlesLocale,bool>> expression ,ChangesType trackChanges)
        => await FindByCondition(expression, trackChanges).ToListAsync();

    public void CreateArticlesLocale(ArticlesLocale articleLocale) => Create(articleLocale);
    public void UpdateArticlesLocale(ArticlesLocale articleLocale) => Update(articleLocale);
    public void DeleteArticlesLocale(ArticlesLocale articleLocale) => Delete(articleLocale);
}