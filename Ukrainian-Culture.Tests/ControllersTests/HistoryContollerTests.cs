using NSubstitute;

namespace Ukrainian_Culture.Tests.ControllersTests;

public class HistoryControllerTests
{
    private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
    private readonly IMapper _mapper;

    public HistoryControllerTests()
    {
        _mapper = new Mapper(new MapperConfiguration(conf => conf.AddProfile(new MappingProfile())));
    }

    [Fact]
    public async Task GetHistoryOfRegion_ShouldReturnListOfHisrtoryDto_WhenDbIsNotEmpty()
    {
        //Arrange
        var controller = new HistoryController(_mapper, _repositoryManager);

        _repositoryManager.Articles
            .GetArticlesByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>()
            {
                new()
                {
                    Id = 0,
                    Type = "file",
                    Region = "Kyiv",
                    Date = new DateTime(2003, 01, 01),
                    CategoryId = 1,
                }
            });

        _repositoryManager.ArticleLocales
            .GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<ArticlesLocale>()
            {
                new()
                {
                    Id = 1,
                    CultureId = 1,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                }
            }
            );

        //Act
        var result = await controller.GetHistoryByRegion(1, "Kyiv") as OkObjectResult;

        var code = result!.StatusCode;
        var resultIEnumerable = (IEnumerable<HistoryDto>)result.Value!;

        //Assert
        code.Should().Be((int)HttpStatusCode.OK);
        resultIEnumerable.Should().HaveCount(1);
    }

    [Fact]
    public void GetHistoryOfRegion_ShouldThrowExceprion_WhenArticleDbIsEmpty()
    {
        //Arrange
        var controller = new HistoryController(_mapper, _repositoryManager);

        _repositoryManager.Articles
            .GetArticlesByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(Enumerable.Empty<Article>());

        _repositoryManager.ArticleLocales.GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<ArticlesLocale>()
            {
                new()
                {
                    Id=1
                }
            });

        //Assert and act
        Assert.ThrowsAsync<Exception>(() => controller.GetHistoryByRegion(1, "Kyiv"));
    }

    [Fact]
    public void GetHistoryOfRegion_ShouldThrowExceprion_WhenArticlesLocaleDbIsEmpty()
    {
        //Arrange
        var controller = new HistoryController(_mapper, _repositoryManager);

        _repositoryManager.Articles
            .GetArticlesByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>()
            {
                new()
                {
                    Id = 0,
                    Type = "file",
                    Region = "Kyiv",
                    Date = new DateTime(2003, 01, 01),
                    CategoryId = 1,
                }
            });

        _repositoryManager.ArticleLocales.GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(Enumerable.Empty<ArticlesLocale>());

        //Assert and act
        Assert.ThrowsAsync<Exception>(() => controller.GetHistoryByRegion(1, "Kyiv"));
    }
    [Fact]
    public async Task GetHistoryOfRegion_ShouldReturnCorrectResult_WhenArticleDbAndArticlesLocaleDbAreNotEmpty()
    {
        //Arrange
        _repositoryManager.Articles
            .GetArticlesByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<Article>()
            {
                new()
                {
                    Id = 1,
                    Type = "file",
                    Region = "Kyiv",
                    Date = new DateTime(2003, 01, 01),
                    CategoryId = 1,
                }
            });

        _repositoryManager.ArticleLocales
            .GetArticlesLocaleByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<ArticlesLocale>()
            {
                new()
                {
                    Id = 1,
                    CultureId = 1,
                    Title = "About Bohdan Khmelnytsky",
                    Content = "About Bohdan Khmelnytsky .... ",
                    SubText = "About Bohdan Khmelnytsky",
                    ShortDescription = "About Bohdan Khmelnytsky"
                },
                 new()
                {
                    Id = 1,
                    CultureId = 2,
                    Title = "Про Богдана Хмельницького",
                    Content = "Про Богдана Хмельницького .... ",
                    SubText = "Про Богдана Хмельницького",
                    ShortDescription = "Про Богдана Хмельницького"
                }
            });

        var controller = new HistoryController(_mapper, _repositoryManager);

        //Act
        var result = await controller.GetHistoryByRegion(1, "Kyiv") as OkObjectResult;

        var statusCode = result!.StatusCode;
        var resultArray = ((IEnumerable<HistoryDto>)result.Value!).ToList();

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK); //HttpStatusCode.OK = 200
        resultArray.Should().HaveCount(1);

        resultArray[0].Should().Match<HistoryDto>(art =>
            art.ActicleId == 1 &&
            art.Date == "01.01.2003" &&
            art.Region == "Kyiv" &&
            art.ShortDescription == "About Bohdan Khmelnytsky");
    }
}