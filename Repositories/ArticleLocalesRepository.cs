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

    public async Task<IEnumerable<ArticlesLocale>> GetArticlesLocaleByConditionAsync(Expression<Func<ArticlesLocale, bool>> expression, ChangesType trackChanges)
        => await FindByCondition(expression, trackChanges).ToListAsync();

    public async Task<ArticlesLocale?> GetFirstByConditionAsync(Expression<Func<ArticlesLocale, bool>> func, ChangesType trackChanges)
        => await FindByCondition(func, trackChanges).SingleOrDefaultAsync();

    public void CreateArticlesLocaleForCulture(Guid cultureId, ArticlesLocale article)
    {
        article.CultureId = cultureId;
        article.Id = Guid.NewGuid();
        Create(article);
    }

    public void UpdateArticlesLocale(ArticlesLocale articleLocale) => Update(articleLocale);
    public void DeleteArticlesLocale(ArticlesLocale articleLocale) => Delete(articleLocale);
}