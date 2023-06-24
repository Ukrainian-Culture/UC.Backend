using System.Linq.Expressions;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Ukranian_Culture.Backend.Services;

public sealed class CachingArticleTileService : IArticleTileService
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