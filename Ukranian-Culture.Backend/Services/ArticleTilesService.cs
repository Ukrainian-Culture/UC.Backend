using System.Linq.Expressions;
using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;
using Repositories.CachingRepository;

namespace Ukranian_Culture.Backend.Services;

public sealed class ArticleTilesService : IArticleTileService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    public ArticleTilesService(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ArticleTileDto>> TryGetArticleTileDto(Guid cultureId,
        Expression<Func<Article, bool>> conditionToFindArticles)
    {
        var articlesLocales = await GetArticlesLocaleByCondition(artL => artL.CultureId == cultureId);
        var articles = await GetArticlesByConditionAsync(conditionToFindArticles);
        return await CreateArticleTileDtos(articlesLocales, articles);
    }

    private Task<List<ArticleTileDto>> CreateArticleTileDtos(IEnumerable<ArticlesLocale> articlesLocale,
        IEnumerable<Article> articles)
    {
        var articleTiles = new List<ArticleTileDto>();
        foreach (var article in articles)
        {
            ArticlesLocale? correctArticleLocale = articlesLocale.FirstOrDefault(artL => artL.Id == article.Id);
            if (correctArticleLocale is not null)
            {
                articleTiles.Add(_mapper.Map<ArticleTileDto>((article, correctArticleLocale)));
                continue;
            }

            _logger.LogError($"article with id:\"{article.Id}\" has no translate, or {article.CategoryId} is invalid");
        }

        return Task.FromResult(articleTiles);
    }

    private async Task<IEnumerable<Article>> GetArticlesByConditionAsync(Expression<Func<Article, bool>> expression)
        => await _repositoryManager
            .Articles
            .GetAllByConditionAsync(expression, ChangesType.AsNoTracking);

    private async Task<IEnumerable<ArticlesLocale>> GetArticlesLocaleByCondition(
        Expression<Func<ArticlesLocale, bool>> func)
        => await _repositoryManager
            .ArticleLocales
            .GetArticlesLocaleByConditionAsync(func, ChangesType.AsNoTracking);
}

public class CachingArticleTileService : IArticleTileService
{
    private readonly IArticleTileService _articleTileService;
    private readonly CachingHelperService _cachingHelper;

    public CachingArticleTileService(IArticleTileService articleTileService, IMemoryCache memoryCache)
    {
        _articleTileService = articleTileService;
        _cachingHelper = new CachingHelperService("ArticleTile", memoryCache);
    }

    public Task<IEnumerable<ArticleTileDto>> TryGetArticleTileDto
        (Guid cultureId, Expression<Func<Article, bool>> conditionToFindArticles)
        => _cachingHelper.GetCachedResultAsync(
            conditionToFindArticles.ToString(),
            () => _articleTileService.TryGetArticleTileDto(cultureId, conditionToFindArticles)!)!;
}