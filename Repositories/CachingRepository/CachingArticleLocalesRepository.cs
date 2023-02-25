using System.Linq.Expressions;
using Contracts;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Repositories.CachingRepository;

public class CachingArticleLocalesRepository : IArticleLocalesRepository
{
    private readonly IArticleLocalesRepository _articleLocales;
    private readonly CachingHelperService _cachingHelper;

    public CachingArticleLocalesRepository(IArticleLocalesRepository articleLocales, IMemoryCache cache)
    {
        _articleLocales = articleLocales;
        _cachingHelper = new CachingHelperService("ArticleLocales", cache);
    }

    public Task<IEnumerable<ArticlesLocale>> GetArticlesLocaleByConditionAsync(
        Expression<Func<ArticlesLocale, bool>> expression, ChangesType trackChanges)
        => _cachingHelper.GetCachedResultAsync(expression.ToString(),
            () => _articleLocales.GetArticlesLocaleByConditionAsync(expression, trackChanges)!)!;
    
    public Task<ArticlesLocale?> GetFirstByConditionAsync(Expression<Func<ArticlesLocale, bool>> func,
        ChangesType trackChanges)
        => _cachingHelper.GetCachedResultAsync(func.ToString(),
            () => _articleLocales.GetFirstByConditionAsync(func, trackChanges));

    public void CreateArticlesLocaleForCulture(Guid cultureId, ArticlesLocale article)
        => _articleLocales.CreateArticlesLocaleForCulture(cultureId, article);

    public void UpdateArticlesLocale(ArticlesLocale article) => _articleLocales.UpdateArticlesLocale(article);

    public void DeleteArticlesLocale(ArticlesLocale article) => _articleLocales.DeleteArticlesLocale(article);
}