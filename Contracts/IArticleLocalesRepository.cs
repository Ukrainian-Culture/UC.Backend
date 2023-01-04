using Entities.Models;

namespace Contracts;

public interface IArticleLocalesRepository
{
    public Task<ArticlesLocale> GetArticlesLocaleByIdAsync(int id, ChangesType trackChanges);
    public IEnumerable<ArticlesLocale> GetAllArticlesLocale(ChangesType trackChanges);

    void CreateArticlesLocale(ArticlesLocale article);
    void UpdateArticlesLocale(ArticlesLocale article);
    void DeleteArticlesLocale(ArticlesLocale article);
}