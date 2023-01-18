using Microsoft.AspNetCore.SignalR;

namespace Ukrainian_Culture.Tests.ControllersTests;

public class ArticleTileControllerTests
{
    private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
    [Fact]
    public async Task GetAllArticlesOnLanguage_ShouldReturnListOfArticlesTileDto_WhenDbIsNotEmpty()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        _repositoryManager.Articles
                .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
                .Returns(new List<Article>()
                {
                new()
                {
                    Id = articleId,
                    CategoryId = categoryId,
                }
                });

        _repositoryManager.Cultures.GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking)
            .Returns(new Culture()
            {
                Id = cultureId,
                ArticlesTranslates = new List<ArticlesLocale>()
                {
                    new()
                    {
                        Id = articleId,
                        CultureId = cultureId,
                    }
                },
                Categories = new List<CategoryLocale>()
                {
                    new()
                    {
                        CategoryId = categoryId,
                        CultureId = cultureId,
                    }
                }
            });

        var controller = new ArticlesTileController(_repositoryManager, _mapper, _logger);

        //Act
        var result = await controller.GetAllArticlesOnLanguage(cultureId) as OkObjectResult;
        var statusCode = result!.StatusCode;
        var resultArray = (IEnumerable<ArticleTileDto>)result.Value!;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK); //HttpStatusCode.OK = 200
        resultArray.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAllArticlesOnLanguage_ShouldReturnEmptyList_WhenArticlesAndCategoriesAreEmpty()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(Enumerable.Empty<Article>());

        _repositoryManager.Cultures.GetCultureWithContentAsync(Arg.Any<Guid>(), Arg.Any<ChangesType>())
            .Returns(new Culture()
            {
                Id = cultureId,
                ArticlesTranslates = Enumerable.Empty<ArticlesLocale>().ToList(),
                Categories = Enumerable.Empty<CategoryLocale>().ToList()
            });

        var controller = new ArticlesTileController(_repositoryManager, _mapper, _logger);

        //Act
        var result = await controller.GetAllArticlesOnLanguage(cultureId) as OkObjectResult;
        var statusCode = result!.StatusCode;
        var resultArray = (IEnumerable<ArticleTileDto>)result.Value!;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK); //HttpStatusCode.OK = 200
        resultArray.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllArticlesOnLanguage_ShouldThrowException_WhenArticlesAreNull()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        var controller = new ArticlesTileController(_repositoryManager, _mapper, _logger);

        try
        {
            //Act
            _ = await controller.GetAllArticlesOnLanguage(cultureId) as OkObjectResult;
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<NullReferenceException>();
        }
    }

    [Fact]
    public async Task GetAllArticlesOnLanguage_SholudReturnCorrectResult_WhenAllModelsAreCorrect()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>()
            {
                new()
                {
                    Id = articleId,
                    CategoryId = categoryId,
                    Region = "test",
                    Type = "test"
                }
            });

        _repositoryManager.Cultures.GetCultureWithContentAsync(cultureId, ChangesType.AsNoTracking)
            .Returns(new Culture()
            {
                Id = cultureId,
                ArticlesTranslates = new List<ArticlesLocale>()
                {
                    new()
                    {
                        Id = articleId,
                        CultureId = cultureId,
                        SubText = "test",
                        Title = "test"
                    }
                },
                Categories = new List<CategoryLocale>()
                {
                    new()
                    {
                        CategoryId = categoryId,
                        CultureId = cultureId,
                        Name = "test"
                    }
                }
            });

        var controller = new ArticlesTileController(_repositoryManager, _mapper, _logger);

        //Act
        var result = await controller.GetAllArticlesOnLanguage(cultureId) as OkObjectResult;
        var statusCode = result!.StatusCode;
        var resultArray = ((IEnumerable<ArticleTileDto>)result.Value!).ToList();

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK); //HttpStatusCode.OK = 200
        resultArray.Should().HaveCount(1);
        _mapper.ReceivedCalls().Should().HaveCount(1);
    }
}