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

    public async Task<IEnumerable<Article>> GetArticlesByRegoinAsync(string region, ChangesType trackChanges)
        => await FindByCondition(art => art.Region == region, trackChanges)
            .ToListAsync();

    public async Task<Article> GetArticleByIdAsync(int id, ChangesType trackChanges)
        => await FindByCondition(art => art.Id == id, trackChanges)
            .SingleAsync();

    public void CreateArticle(Article article) => Create(article);
    public void UpdateArticle(Article article) => Update(article);
    public void DeleteArticle(Article article) => Delete(article);
}