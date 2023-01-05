using Entities.Models;
using System.Linq.Expressions;

namespace Contracts;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetArticlesByCondition(Expression<Func<Article, bool>> expression, ChangesType trackChanges);
    Task<Article> GetArticleByIdAsync(int id, ChangesType trackChanges);

    void CreateArticle(Article article);
    void UpdateArticle(Article article);
    void DeleteArticle(Article article);
}
