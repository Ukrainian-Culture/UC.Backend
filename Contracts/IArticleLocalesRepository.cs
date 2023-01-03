using Entities.Models;

namespace Contracts;

public interface IArticleLocalesRepository
{
    public Task<ArticlesLocale> GetArticlesLocaleByIdAsync(int id, ChangesType trackChanges);

    void CreateArticlesLocale(ArticlesLocale article);
    void UpdateArticlesLocale(ArticlesLocale article);
    void DeleteArticlesLocale(ArticlesLocale article);
}