using Entities.Models;

namespace Contracts;

public interface IArticleLocalesRepository
{
    Task<IEnumerable<ArticlesLocale>> GetTileDataOfArticlesAsync(int cultureId, ChangesType asNoTracking);
}