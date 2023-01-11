using System.Linq.Expressions;
using Entities.Models;


namespace Contracts;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetAllByConditionAsync(Expression<Func<Article, bool>> expression,
        ChangesType trackChanges);

    Task<Article?> GetFirstByConditionAsync(Expression<Func<Article, bool>> expression, ChangesType trackChanges);

    void CreateArticle(Article article);
    void UpdateArticle(Article article);
    void DeleteArticle(Article article);
}

