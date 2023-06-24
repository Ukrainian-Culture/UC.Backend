using Microsoft.AspNetCore.SignalR;

namespace Ukrainian_Culture.Tests.ControllersTests;

public class ArticleTileControllerTests
{
    private readonly IArticleTileService _articleTileService = Substitute.For<IArticleTileService>();

    [Fact]
    public async Task GetAllArticlesOnLanguage_ShouldReturnListOfArticlesTileDto_WhenDbIsNotEmpty()
    {
        //Arrange
        _articleTileService.TryGetArticleTileDto(Arg.Any<Guid>(),
                Arg.Any<Expression<Func<Article, bool>>>())
            .Returns(new List<ArticleTileDto>());
        var controller = new ArticlesTileController(_articleTileService);

        //Act
        var cultureId = Guid.NewGuid();
        var result = await controller.GetAllArticlesOnLanguage(cultureId) as OkObjectResult;
        var statusCode = result!.StatusCode;
        var resultArray = (IEnumerable<ArticleTileDto>)result.Value!;
        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK); //HttpStatusCode.OK = 200
        resultArray.Should().NotBeNull();
    }


    [Fact]
    public async Task GetArticlesTileForEveryCategory_ShouldReturnOkWithCorrectValue_WhenAllDaraIsCorect()
    {
        //Arrange
        Guid cultureId = new("8ae09631-e4f3-4fce-9025-04c2d00d2ab1");

        _articleTileService
            .TryGetArticleTileDto(cultureId, Arg.Any<Expression<Func<Article, bool>>>())
            .Returns(new List<ArticleTileDto>
            {
                new()
                {
                    ArticleId = new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203"),
                    Category = "first",
                },
                new()
                {
                    ArticleId = new Guid("b05ab052-1e09-473b-b0c9-9355ac21f1bb"),
                    Category = "first"
                }
            });
        var controller = new ArticlesTileController(_articleTileService);

        //Act
        var result = await controller.GetArticlesTileForEveryCategory(cultureId) as OkObjectResult;
        var statusCode = result!.StatusCode;
        var value = (Dictionary<string, List<ArticleTileDto>>)result.Value!;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
        value.Should().HaveCount(1);
        value["first"].Should().HaveCount(2);
    }

    [Fact]
    public async Task GetArticlesTileForEveryCategory_ShouldReturnOkValuesWithEmptyList_WhenDictionaryIsEmpty()
    {
        //Arrange
        Guid cultureId = new("8ae09631-e4f3-4fce-9025-04c2d00d2ab1");

        _articleTileService
            .TryGetArticleTileDto(cultureId, Arg.Any<Expression<Func<Article, bool>>>())
            .Returns(Enumerable.Empty<ArticleTileDto>());

        var controller = new ArticlesTileController(_articleTileService);

        //Act
        var result = await controller.GetArticlesTileForEveryCategory(cultureId) as OkObjectResult;
        var statusCode = result!.StatusCode;
        var value = (Dictionary<string, List<ArticleTileDto>>)result.Value!;

        //Assert
        value.Should().BeEmpty();
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }
}