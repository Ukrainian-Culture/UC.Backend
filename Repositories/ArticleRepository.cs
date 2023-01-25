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
        => trackChanges switch
        {
            ChangesType.AsNoTracking => await Context
                .Articles
                .AsNoTracking()
                .Where(expression)
                .Include(a => a.Category)
                .ToListAsync(),

            ChangesType.Tracking => await Context
                .Articles
                .Where(expression)
                .Include(a => a.Category)
                .ToListAsync(),

            _ => throw new InvalidOperationException()
        };
    public async Task<Article?> GetFirstByConditionAsync(Expression<Func<Article, bool>> expression, ChangesType trackChanges)
        => await Context.Articles.AsNoTracking().Where(expression).Include(a => a.Category).FirstOrDefaultAsync();
    public void CreateArticle(Article article) => Create(article);
    public void UpdateArticle(Article article) => Update(article);
    public void DeleteArticle(Article article) => Delete(article);
    public Task<int> CountAsync => Context.Articles.CountAsync();
}