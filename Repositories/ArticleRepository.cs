using System.Linq.Expressions;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
{
    public ArticleRepository(RepositoryContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Article>> GetAllByConditionAsync(Expression<Func<Article, bool>> expression, ChangesType trackChanges)
        => await FindByCondition(expression, trackChanges).ToListAsync();

    public void CreateArticle(Article article) => Create(article);
    public void UpdateArticle(Article article) => Update(article);
    public void DeleteArticle(Article article) => Delete(article);
}