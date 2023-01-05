using Entities.Models;
using System.Linq.Expressions;

namespace Contracts;

public interface IArticleLocalesRepository
{
    public Task<ArticlesLocale> GetArticlesLocaleByIdAsync(int id, ChangesType trackChanges);

    Task<IEnumerable<ArticlesLocale>> GetArticlesLocaleByCondition(Expression<Func<ArticlesLocale, bool>> expression, ChangesType trackChanges);

    void CreateArticlesLocale(ArticlesLocale article);
    void UpdateArticlesLocale(ArticlesLocale article);
    void DeleteArticlesLocale(ArticlesLocale article);
}