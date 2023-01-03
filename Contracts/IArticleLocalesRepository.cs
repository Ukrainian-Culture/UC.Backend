using Entities.Models;

namespace Contracts;

public interface IArticleLocalesRepository
{
    public Task<ArticlesLocale> GetArticleByIdAsync(int id, ChangesType trackChanges);
}