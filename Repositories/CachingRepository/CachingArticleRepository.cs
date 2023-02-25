using System.Linq.Expressions;
using Contracts;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Repositories.CachingRepository;

public class CachingArticleRepository : IArticleRepository
{
    private readonly ArticleRepository _articleRepository;
    private readonly CachingHelperService _cachingHelper;
    
    public CachingArticleRepository(ArticleRepository articleRepository, IMemoryCache memoryCache)
    {
        _articleRepository = articleRepository;
        _cachingHelper = new CachingHelperService("Articles", memoryCache);
    }

    public Task<IEnumerable<Article>> GetAllByConditionAsync(Expression<Func<Article, bool>> expression,
        ChangesType trackChanges)
        => _cachingHelper.GetCachedResultAsync(expression.ToString(),
            () => _articleRepository.GetAllByConditionAsync(expression, trackChanges)!)!;

    public Task<Article?> GetFirstByConditionAsync(Expression<Func<Article, bool>> expression,
        ChangesType trackChanges)
        => _cachingHelper.GetCachedResultAsync(expression.ToString(),
            () => _articleRepository.GetFirstByConditionAsync(expression, trackChanges));

    public void CreateArticle(Article article) => _articleRepository.CreateArticle(article);
    public void UpdateArticle(Article article) => _articleRepository.UpdateArticle(article);
    public void DeleteArticle(Article article) => _articleRepository.DeleteArticle(article);
    public Task<int> CountAsync => _articleRepository.CountAsync;
}