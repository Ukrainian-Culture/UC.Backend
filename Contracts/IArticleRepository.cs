using Entities.Models;
using System.Linq.Expressions;

namespace Contracts;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetArticlesByConditionAsync(Expression<Func<Article, bool>> expression, ChangesType trackChanges);

    void CreateArticle(Article article);
    void UpdateArticle(Article article);
    void DeleteArticle(Article article);
}
