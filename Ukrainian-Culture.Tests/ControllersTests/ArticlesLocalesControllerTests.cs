using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ukranian_Culture.Backend.ActionFilters.ArticleLocaleActionFilters;
using RouteData = Microsoft.AspNetCore.Routing.RouteData;

namespace Ukrainian_Culture.Tests.ControllersTests;

public class ArticlesLocalesControllerTests
{
    private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
    private readonly IErrorMessageProvider _messageProvider = Substitute.For<IErrorMessageProvider>();
    /*
    [Fact]
    public async Task GetAllArticlesLocales_ShouldReturnNotFound_WhenCultureDoesnotExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.GetAllArticlesLocales(cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task GetArticleLocaleById_SholudReturnNotFoundAndLoggingResult_WhenCultureDontExistInDB()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.GetArticleLocaleById(new Guid(), cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task GetArticleLocaleById_SholudReturnNotFoundAndLoggingResult_WhenRecievesUncorrectId()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unCorrectId = new Guid();
        var result = await controller.GetArticleLocaleById(unCorrectId, cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task GetArticleLocaleById_SholudReturnOkResult_WhenRecievesCorrectIdAndCultureExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new ArticlesLocale());

        _repositoryManager.Articles
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Article, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Article());

        _mapper.Map<ArticlesLocaleToGetDto>(Arg.Any<ArticlesLocale>())
            .Returns(new ArticlesLocaleToGetDto());

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.GetArticleLocaleById(new Guid(), cultureId) as OkObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateArticleLocale_SholudReturnBadRequestAndLogging_WhenArticleLocaleIsNull()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);
        //Act
        var result = await controller.CreateArticleLocale(null, cultureId) as BadRequestObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.BadRequest);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task CreateArticleLocale_SholudReturnNotFoundAndLogging_WhenCultureDontExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result =
            await controller.CreateArticleLocale(new ArticleLocaleToCreateDto(), cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task CreateArticleLocale_SholudReturnOkResult_WhenArticleLocaleIsNotNullAndCultureExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager
            .Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.CreateArticleLocale(new ArticleLocaleToCreateDto(), cultureId) as OkObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
        _mapper.ReceivedCalls().Should().HaveCount(1);
        _repositoryManager.ArticleLocales.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteArticleLocale_SholudReturnNotFoundAndLogging_WhenCultureDontExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.DeleteArticleLocale(new Guid(), cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteArticleLocale_SholudReturnNotFoundAndLogging_WhenArticleNotFoundIdDb()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unrealId = new Guid();
        var result = await controller.DeleteArticleLocale(unrealId, cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteArticleLocale_SholudReturnNoContent_WhenArticleContainsInDb()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new ArticlesLocale());

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var idOfArticleWhichContain = new Guid();
        var result = await controller.DeleteArticleLocale(idOfArticleWhichContain, cultureId) as NoContentResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NoContent);
        _repositoryManager.ArticleLocales.ReceivedCalls().Should().HaveCount(2);
    }

    [Fact]
    public async Task UpdateArticleLocale_ShouldReturnBadRequestAndLogging_WhenRecieveNull()
    {
        //Arrange
        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.UpdateArticleLocale(new Guid(), null, Guid.Empty);
        var statusCode = (result as BadRequestObjectResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.BadRequest);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task UpdateArticleLocale_ShouldReturnNotFoundAndLogging_WhenCultureDontExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result =
            await controller.UpdateArticleLocale(new Guid(), new ArticleLocaleToUpdateDto(), cultureId) as
                NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task UpdateArticleLocale_ShouldReturnNotFoundAndLogging_WhenArticleRecievedIdWhichNotContainInDb()
    {
        //Arrange

        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();
        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);
        var actionFilter = new ArticleLocaleExistAttribute(_repositoryManager, _messageProvider, _logger);

        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());
        var actionExecutingContext = new ActionExecutingContext(actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>()!,
            controller: controller);

        ActionExecutionDelegate mockDelegate = ()
            => Task.FromResult(new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), controller));

        await actionFilter.OnActionExecutionAsync(actionExecutingContext, mockDelegate);

        //Act
        var unCorrectId = new Guid();
        var result =
            await controller.UpdateArticleLocale(unCorrectId, new ArticleLocaleToUpdateDto(), cultureId) as
                NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task UpdateArticleLocale_ShouldReturnNoContent_WhenAllIsGood()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new ArticlesLocale());

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unCorrectId = new Guid();
        var result = await controller.UpdateArticleLocale(unCorrectId, new ArticleLocaleToUpdateDto(), cultureId);
        var statusCode = (result as NoContentResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NoContent);
        _mapper.ReceivedCalls().Should().HaveCount(1);
        _repositoryManager.ArticleLocales.ReceivedCalls().Should().HaveCount(1);
    }
    */
        [Fact]
    public async Task GetArticleLocalePDFById_SholudReturnNotFoundAndLoggingResult_WhenCultureDontExistInDB()
    {
        //Arrange
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();
            
        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);
        
        //Act
        var unCorrectId = new Guid();
        var result = await controller.GetArticleLocalePdfById(new Guid(), new Guid()) as NotFoundResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetArticleLocalePDFById_SholudReturnNotFoundAndLoggingResult_WhenRecievesUncorrectId()
    {
        //Arrange
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);
        
        //Act
        var unCorrectId = new Guid();
        var result = await controller.GetArticleLocalePdfById(new Guid(), new Guid()) as NotFoundResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetArticleLocalePDFById_SholudReturnOkResult_WhenRecievesCorrectIdAndCultureExist()
    {
        //Arrange
        _repositoryManager.Cultures
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<Culture, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.ArticleLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<ArticlesLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new ArticlesLocale()
            {
                Content = "content",
                Title = "title"
            });
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        var controller = new ArticlesLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);
        
        //Act
        var unCorrectId = new Guid();
        var result = await controller.GetArticleLocalePdfById(new Guid(), new Guid()) as FileContentResult;

        //Assert
        result.Should().NotBeNull();
    }
}