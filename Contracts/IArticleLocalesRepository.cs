using Entities.Models;

namespace Contracts;

public interface IArticleLocalesRepository
{
    public Task<ArticlesLocale> GetArticleLocaleByIdAsync(int id, ChangesType trackChanges);
}