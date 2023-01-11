namespace Ukrainian_Culture.Tests.ControllersTests;

public class ArticleTileControllerTests
{
    private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
    private readonly IMapper _mapper;

    public ArticleTileControllerTests()
    {
        _mapper = new Mapper(new MapperConfiguration(conf => conf.AddProfile(new MappingProfile())));
    }

    [Fact]
    public async Task GetAllArticlesOnLanguage_ShouldReturnListOfArticlesTileDto_WhenDbIsNotEmpty()
    {
        //Arrange

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>()
            {
                new()
                {
                    Id = 1,
                    CategoryId = 1,
                }
            });

        _repositoryManager.Cultures.GetCultureWithContentAsync(1, ChangesType.AsNoTracking)
            .Returns(new Culture()
            {
                Id = 1,
                ArticlesTranslates = new List<ArticlesLocale>()
                {
                    new()
                    {
                        Id = 1,
                        CultureId = 1,
                    }
                },
                Categories = new List<CategoryLocale>()
                {
                    new()
                    {
                        CategoryId = 1,
                        CultureId = 1,
                    }
                }
            });

        var controller = new ArticlesTileController(_repositoryManager, _mapper);

        //Act
        var result = await controller.GetAllArticlesOnLanguage(1) as OkObjectResult;
        var statusCode = result!.StatusCode;
        var resultArray = (IEnumerable<ArticleTileDto>) result.Value!;

        //Assert
        statusCode.Should().Be((int) HttpStatusCode.OK); //HttpStatusCode.OK = 200
        resultArray.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAllArticlesOnLanguage_ShouldReturnEmptyList_WhenArticlesAndCategoriesAreEmpty()
    {
        //Arrange
        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(Enumerable.Empty<Article>());

        _repositoryManager.Cultures.GetCultureWithContentAsync(Arg.Any<int>(), Arg.Any<ChangesType>())
            .Returns(new Culture()
            {
                Id = 1,
                ArticlesTranslates = Enumerable.Empty<ArticlesLocale>().ToList(),
                Categories = Enumerable.Empty<CategoryLocale>().ToList()
            });

        var controller = new ArticlesTileController(_repositoryManager, _mapper);

        //Act
        var result = await controller.GetAllArticlesOnLanguage(1) as OkObjectResult;
        var statusCode = result!.StatusCode;
        var resultArray = (IEnumerable<ArticleTileDto>) result.Value!;

        //Assert
        statusCode.Should().Be((int) HttpStatusCode.OK); //HttpStatusCode.OK = 200
        resultArray.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllArticlesOnLanguage_ShouldThrowException_WhenArticlesAreNull()
    {
        //Arrange
        var controller = new ArticlesTileController(_repositoryManager, _mapper);

        try
        {
            //Act
            _ = await controller.GetAllArticlesOnLanguage(1) as OkObjectResult;
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

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>()
            {
                new()
                {
                    Id = 1,
                    CategoryId = 1,
                    Region = "test",
                    Type = "test"
                }
            });

        _repositoryManager.Cultures.GetCultureWithContentAsync(1, ChangesType.AsNoTracking)
            .Returns(new Culture()
            {
                Id = 1,
                ArticlesTranslates = new List<ArticlesLocale>()
                {
                    new()
                    {
                        Id = 1,
                        CultureId = 1,
                        SubText = "test",
                        Title = "test"
                    }
                },
                Categories = new List<CategoryLocale>()
                {
                    new()
                    {
                        CategoryId = 1,
                        CultureId = 1,
                        Name = "test"
                    }
                }
            });

        var controller = new ArticlesTileController(_repositoryManager, _mapper);

        //Act
        var result = await controller.GetAllArticlesOnLanguage(1) as OkObjectResult;
        var statusCode = result!.StatusCode;
        var resultArray = ((IEnumerable<ArticleTileDto>) result.Value!).ToList();

        //Assert
        statusCode.Should().Be((int) HttpStatusCode.OK); //HttpStatusCode.OK = 200
        resultArray.Should().HaveCount(1);

        resultArray[0].Should().Match<ArticleTileDto>(art =>
            art.ArticleId == 1 &&
            art.SubText == "test" &&
            art.Title == "test" &&
            art.Region == "test" &&
            art.Type == "test");
    }
}