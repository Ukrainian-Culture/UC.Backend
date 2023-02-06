using System.Linq.Expressions;
using Entities.DTOs;
using Entities.Models;

namespace Contracts;

public interface IArticleTileService
{
    Task<IEnumerable<ArticleTileDto>> TryGetArticleTileDto(Guid cultureId,
        Expression<Func<Article, bool>> conditionToFindArticles);
}