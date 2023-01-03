using Contracts;
using Entities;

namespace Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private IUserRepository? _userRepository;
    private IArticleLocalesRepository? _articleLocalesRepository;
    private IArticleRepository? _articleRepository;
    private ICategoryLocalesRepository? _categoryLocalesRepository;
    private ICategoryRepository? _categoryRepository;
    private ICultureRepository? _cultureRepository;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public IUserRepository Users
        => _userRepository ??= new UserRepository(_repositoryContext);

    public IArticleLocalesRepository ArticleLocales
        => _articleLocalesRepository ??= new ArticleLocalesRepository(_repositoryContext);

    public IArticleRepository Articles
        => _articleRepository ??= new ArticleRepository(_repositoryContext);

    public ICategoryLocalesRepository CategoryLocales =>
        _categoryLocalesRepository ??= new CategoryLocalesRepository(_repositoryContext);

    public ICategoryRepository Categories
        => _categoryRepository ??= new CategoryRepository(_repositoryContext);

    public ICultureRepository Cultures
        => _cultureRepository ??= new CultureRepository(_repositoryContext);

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}