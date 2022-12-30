namespace Contracts;

public interface IRepositoryManager
{
    IUserRepository Users { get; }
    IArticleLocalesRepository ArticleLocales { get; }
    IArticleRepository Articles { get; }
    ICategoryLocalesRepository CategoryLocales { get; }
    ICategoryRepository Categories { get; }
    IInfoRepository Infos { get; }
    Task SaveAsync();
}