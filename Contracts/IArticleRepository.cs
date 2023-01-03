using System.Linq.Expressions;
using Entities.Models;

namespace Contracts;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetAllArticlesAsync(ChangesType trackChanges);

    Task<IEnumerable<Article>> GetAllByConditionAsync(Expression<Func<Article, bool>> expression,
        ChangesType trackChanges);
}