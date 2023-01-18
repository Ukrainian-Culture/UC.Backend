using Entities.Models;
using System.Linq.Expressions;

namespace Contracts;

public interface IArticleLocalesRepository
{
    Task<IEnumerable<ArticlesLocale>> GetArticlesLocaleByConditionAsync(Expression<Func<ArticlesLocale, bool>> expression, ChangesType trackChanges);
    Task<ArticlesLocale?> GetFirstByConditionAsync(Expression<Func<ArticlesLocale, bool>> func, ChangesType trackChanges);
    void CreateArticlesLocaleForCulture(Guid cultureId, ArticlesLocale article);
    void UpdateArticlesLocale(ArticlesLocale article);
    void DeleteArticlesLocale(ArticlesLocale article);
}