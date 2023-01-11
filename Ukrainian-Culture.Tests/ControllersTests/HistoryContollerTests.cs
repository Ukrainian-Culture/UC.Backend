namespace Ukrainian_Culture.Tests.ControllersTests;

public class HistoryControllerTests
{
    private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();


    [Fact]
    public async Task GetHistoryOfRegion_ShouldReturnListOfHisrtoryDto_WhenDbIsNotEmpty()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        var controller = new HistoryController(_mapper, _repositoryManager, _logger);

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>()
            {
                new()
                {
                    Id = articleId,
                    Type = "file",
                    Region = "Kyiv",
                    Date = new DateTime(2003, 01, 01),
                    CategoryId = categoryId,
                }
            });

        _repositoryManager.ArticleLocales
            .GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<ArticlesLocale>()
            {
                new()
                {
                    Id = articleId,
                    CultureId = cultureId,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                }
            }
            );

        //Act
        var result = await controller.GetHistoryByRegion(cultureId, "Kyiv") as OkObjectResult;

        var code = result!.StatusCode;
        var resultIEnumerable = (IEnumerable<HistoryDto>)result.Value!;

        //Assert
        code.Should().Be((int)HttpStatusCode.OK);
        resultIEnumerable.Should().HaveCount(1);
        _mapper.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task GetHistoryOfRegion_ShouldReturnBadRequest_WhenArticleDbIsEmpty()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        var controller = new HistoryController(_mapper, _repositoryManager, _logger);

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(Enumerable.Empty<Article>());

        _repositoryManager.ArticleLocales.GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<ArticlesLocale>()
            {
                new()
                {
                    Id= articleId
                }
            });

        //Act
        var result = (await controller.GetHistoryByRegion(cultureId,"Kyiv")) as BadRequestResult;

        //Assert
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetHistoryOfRegion_ShouldReturnBadRequest_WhenArticlesLocaleDbIsEmpty()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");

        var controller = new HistoryController(_mapper, _repositoryManager, _logger);

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>()
            {
                new()
                {
                    Id = articleId,
                    Type = "file",
                    Region = "Kyiv",
                    Date = new DateTime(2003, 01, 01),
                    CategoryId = categoryId,
                }
            });

        _repositoryManager.ArticleLocales.GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(Enumerable.Empty<ArticlesLocale>());

        //Act
        var result = await controller.GetHistoryByRegion(cultureId, "Kyiv") as BadRequestResult;

        //Assert
        result!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task GetHistoryOfRegion_ShouldReturnCorrectResult_WhenArticleDbAndArticlesLocaleDbAreNotEmpty()
    {
        //Arrange
        Guid articleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid secondCultureId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        Guid firstCultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24111");
        Guid categoryId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");

        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>()
            {
                new()
                {
                    Id = articleId,
                    Type = "file",
                    Region = "Kyiv",
                    Date = new DateTime(2003, 01, 01),
                    CategoryId = categoryId,
                }
            });

        _repositoryManager.ArticleLocales
            .GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<ArticlesLocale>()
            {
                new()
                {
                    Id = articleId,
                    CultureId = firstCultureId,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                },
                 new()
                {
                    Id = articleId,
                    CultureId = secondCultureId,
                    Title = "Про Богдана Хмельницького",
                    Content = "Про Богдана Хмельницького .... ",
                    SubText = "Про Богдана Хмельницького",
                    ShortDescription = "Про Богдана Хмельницького"
                }
            });

        var controller = new HistoryController(_mapper, _repositoryManager, _logger);

        //Act
        var result = await controller.GetHistoryByRegion(firstCultureId, "Kyiv") as OkObjectResult;

        var statusCode = result!.StatusCode;
        var resultArray = ((IEnumerable<HistoryDto>)result.Value!).ToList();

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK); //HttpStatusCode.OK = 200
        resultArray.Should().HaveCount(1);
        _mapper.ReceivedCalls().Should().HaveCount(1);
    }
}