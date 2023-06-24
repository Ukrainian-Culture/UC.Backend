using Ukranian_Culture.Backend.Services;

namespace Ukrainian_Culture.Tests.ServicesTests;

public class ArticleTilesServiceTests
{
    private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();

    [Fact]
    public async Task TryGetArticleTileDto_ShouldReturnListOfArticlesTileDto_WhenDbIsNotEmpty()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>
            {
                new()
                {
                    Id = articleId,
                    CategoryId = new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"),
                }
            });

        _repositoryManager
            .ArticleLocales
            .GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(),
                Arg.Any<ChangesType>())
            .Returns(new List<ArticlesLocale>
            {
                new()
                {
                    Id = articleId,
                    CultureId = cultureId
                }
            });
        var service = new ArticleTilesService(_repositoryManager, _mapper, _logger);

        //Act
        var result = await service.TryGetArticleTileDto(cultureId, _ => true);
        //Assert
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task TryGetArticleTileDto_ShouldReturnEmptyList_WhenArticleHasNoContent()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>
            {
                new()
                {
                    Id = new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"),
                    CategoryId = new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53"),
                }
            });

        _repositoryManager
            .ArticleLocales
            .GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(),
                Arg.Any<ChangesType>())
            .Returns(new List<ArticlesLocale>
            {
                new()
                {
                    Id = Guid.Empty,
                    CultureId = cultureId
                }
            });
        var service = new ArticleTilesService(_repositoryManager, _mapper, _logger);

        //Act
        var result = await service.TryGetArticleTileDto(cultureId, _ => true);
        //Assert
        result.Should().BeEmpty();
        _logger.ReceivedCalls().Should().HaveCount(1);
    }
}