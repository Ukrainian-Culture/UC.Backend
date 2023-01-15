namespace Ukrainian_Culture.Tests.ControllersTests;

public class ArticleControllerTests
{
    private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
    private readonly IErrorMessageProvider _messageProvider = Substitute.For<IErrorMessageProvider>();

    [Fact]
    public async Task GetAllArticles_SholudReturnEmptyList_WhenDbIsEmpty()
    {
        //Arrange
        _repositoryManager.Articles
            .GetAllByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(Enumerable.Empty<Article>());

        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);
        //Act
        var result = await controller.GetAllArticles() as OkObjectResult;
        var statusCode = result!.StatusCode;
        var resultContent = (IEnumerable<Article>)result.Value!;
        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
        resultContent.Should().BeEmpty();
    }

    [Fact]
    public async Task GetArticleById_SholudReturnOkResult_WhenRecievesCorrectId()
    {
        //Arrange
        _repositoryManager.Articles
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Article());

        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.GetArticleById(new Guid());
        var statusCode = (result as OkObjectResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetArticleById_SholudReturnNotFoundAndLoggingResult_WhenRecievesUncorrectId()
    {
        //Arrange
        _repositoryManager.Articles
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unCorrectId = new Guid();
        var result = await controller.GetArticleById(unCorrectId);
        var statusCode = (result as NotFoundObjectResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task CreateArticle_SholudReturnOk_WhenArticleIsNotNull()
    {
        //Arrange
        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.CreateArticle(new ArticleToCreateDto());
        var statusCode = (result as OkObjectResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
        _mapper.ReceivedCalls().Should().HaveCount(1);
        _repositoryManager.Articles.ReceivedCalls().Should().HaveCount(1);
    }
    [Fact]
    public async Task CreateArticle_SholudReturnBadRequestAndLogging_WhenArticleIsNull()
    {
        //Arrange
        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);
        //Act
        var result = await controller.CreateArticle(null);
        var statusCode = (result as BadRequestObjectResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.BadRequest);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteArticle_SholudReturnNotFoundAndLogging_WhenArticleNotFoundIdDb()
    {
        //Arrange
        _repositoryManager.Articles
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();
        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unrealId = new Guid();
        var result = await controller.DeleteArticle(unrealId);
        var statusCode = (result as NotFoundObjectResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }
    [Fact]
    public async Task DeleteArticle_SholudReturnNoContent_WhenArticleContainsInDb()
    {
        //Arrange
        _repositoryManager.Articles
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Article());
        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var idOfArticleWhichContain = new Guid();
        var result = await controller.DeleteArticle(idOfArticleWhichContain);
        var statusCode = (result as NoContentResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NoContent);
        _repositoryManager.Articles.ReceivedCalls().Should().HaveCount(2);
    }

    [Fact]
    public async Task UpdateArticle_ShouldReturnBadRequestAndLogging_WhenRecieveNull()
    {
        //Arrange
        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.UpdateArticle(new Guid(), null);
        var statusCode = (result as BadRequestObjectResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.BadRequest);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }
    [Fact]
    public async Task UpdateArticle_ShouldReturnNotFoundAndLogging_WhenArticleRecievedIdWhichNotContainInDb()
    {
        //Arrange
        _repositoryManager.Articles
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unCorrectId = new Guid();
        var result = await controller.UpdateArticle(unCorrectId, new ArticleToUpdateDto());
        var statusCode = (result as NotFoundObjectResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }
    [Fact]
    public async Task UpdateArticle_ShouldReturnNoContent_WhenAllIsGood()
    {
        //Arrange
        _repositoryManager.Articles
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Article());

        var controller = new ArticlesController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unCorrectId = new Guid();
        var result = await controller.UpdateArticle(unCorrectId, new ArticleToUpdateDto());
        var statusCode = (result as NoContentResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NoContent);
        _mapper.ReceivedCalls().Should().HaveCount(1);
        _repositoryManager.Articles.ReceivedCalls().Should().HaveCount(1);
    }
}