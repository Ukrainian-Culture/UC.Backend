namespace Contracts;

public interface IRepositoryManager
{
    IUserRepository Users { get; }
    IArticleLocalesRepository ArticleLocales { get; }
    IArticleRepository Articles { get; }
    ICategoryRepository Categories { get; }
    ICultureRepository Cultures { get; }
    IUserHistoryRepository UserHistory { get; }
    Task SaveAsync();
}