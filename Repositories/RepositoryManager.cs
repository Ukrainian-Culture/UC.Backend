using Contracts;
using Entities;

namespace Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IArticleLocalesRepository> _articleLocalesRepository;
    private readonly Lazy<IArticleRepository> _articleRepository;
    private readonly Lazy<ICategoryRepository> _categoryRepository;
    private readonly Lazy<ICultureRepository> _cultureRepository;
    private readonly Lazy<IUserHistoryRepository> _userHistoryRepository;

    public RepositoryManager(
        RepositoryContext repositoryContext,
        Lazy<IUserRepository> userRepository,
        Lazy<IArticleLocalesRepository> articleLocalesRepository,
        Lazy<IArticleRepository> articleRepository,
        Lazy<ICategoryRepository> categoryRepository,
        Lazy<ICultureRepository> cultureRepository,
        Lazy<IUserHistoryRepository> userHistoryRepository)
    {
        _repositoryContext = repositoryContext;
        _userRepository = userRepository;
        _articleLocalesRepository = articleLocalesRepository;
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
        _cultureRepository = cultureRepository;
        _userHistoryRepository = userHistoryRepository;
    }

    public IUserRepository Users => _userRepository.Value;

    public IArticleLocalesRepository ArticleLocales => _articleLocalesRepository.Value;

    public IArticleRepository Articles => _articleRepository.Value;

    public ICategoryRepository Categories => _categoryRepository.Value;

    public ICultureRepository Cultures => _cultureRepository.Value;

    public IUserHistoryRepository UserHistory => _userHistoryRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}