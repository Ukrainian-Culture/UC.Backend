using Entities.Models;

namespace Contracts;

public interface IArticleRepository
{
    public Task<IEnumerable<Article>> GetArticlesByRegoinAsync(string region, ChangesType trackChanges);
    public Task<Article> GetArticleByIdAsync(int id, ChangesType trackChanges);

    void CreateArticle(Article article);
    void UpdateArticle(Article article);
    void DeleteArticle(Article article);
}
