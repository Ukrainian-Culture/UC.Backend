using Entities.Models;
using System.Linq.Expressions;

namespace Contracts;

public interface IArticleLocalesRepository
{
    Task<IEnumerable<ArticlesLocale>> GetArticlesLocaleByConditionAsync(Expression<Func<ArticlesLocale, bool>> expression, ChangesType trackChanges);
    void CreateArticlesLocale(ArticlesLocale article);
    void UpdateArticlesLocale(ArticlesLocale article);
    void DeleteArticlesLocale(ArticlesLocale article);
}