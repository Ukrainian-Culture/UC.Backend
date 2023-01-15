using System.Reflection;

namespace Ukrainian_Culture.Tests.ControllersTests;

public class CategoryLocaleControllerTests
{
    private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
    private readonly IErrorMessageProvider _messageProvider = Substitute.For<IErrorMessageProvider>();

    [Fact]
    public async Task GetAllCategoriesLocales_ShouldReturnNotFound_WhenCultureDoesnotExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .ReturnsNull();

        _messageProvider.NotFoundMessage<Culture>(cultureId)
            .Returns($"No {cultureId}");

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.GetAllCategoriesLocales(cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;
        var message = result.Value as string;

        //Assert
        message.Should().Be("No 5eca5808-4f44-4c4c-b481-72d2bdf24203");
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task GetCategoryLocaleById_SholudReturnNotFoundAndLoggingResult_WhenCultureDontExistInDB()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .ReturnsNull();

        _messageProvider.NotFoundMessage<Culture>(cultureId)
            .Returns($"No {cultureId}");

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.GetCategoryLocaleById(new Guid(), cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;
        var message = result.Value as string;

        //Assert
        message.Should().Be("No 5eca5808-4f44-4c4c-b481-72d2bdf24203");
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task GetArticleLocaleById_SholudReturnNotFoundAndLoggingResult_WhenEntityDontExistInDb()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid unCorrectCategoryId = new("5eca5808-4f44-4c4c-0000-72d2bdf24203");

        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.CategoryLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<CategoryLocale, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        _messageProvider.NotFoundMessage<CategoryLocale>(unCorrectCategoryId)
            .Returns($"No {unCorrectCategoryId}");

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act

        var result = await controller.GetCategoryLocaleById(unCorrectCategoryId, cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;
        var message = result.Value as string;

        //Assert
        message.Should().Be("No 5eca5808-4f44-4c4c-0000-72d2bdf24203");
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task GetArticleLocaleById_SholudReturnOkResult_WhenRecievesCorrectIdAndCultureExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.CategoryLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<CategoryLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new CategoryLocale());

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.GetCategoryLocaleById(new Guid(), cultureId) as OkObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateCategoryLocale_SholudReturnBadRequestAndLogging_WhenCategoryLocaleIsNull()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .Returns(new Culture());

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);
        //Act
        var result = await controller.CreateCategoryLocale(null, cultureId) as BadRequestObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.BadRequest);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task CreateCategoryLocale_SholudReturnNotFoundAndLogging_WhenCultureDontExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .ReturnsNull();

        _messageProvider.NotFoundMessage<Culture>(cultureId)
            .Returns($"No {cultureId}");

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.CreateCategoryLocale(new CategoryLocaleToCreateDto(), cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;
        var message = result.Value as string;
        
        //Assert
        message.Should().Be("No 5eca5808-4f44-4c4c-b481-72d2bdf24203");
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteCategoryLocale_SholudReturnNotFoundAndLogging_WhenCultureDontExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.DeleteCategoryLocale(new Guid(), cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteCategoryLocale_SholudReturnNotFoundAndLogging_WhenArticleNotFoundIdDb()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.CategoryLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<CategoryLocale, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unrealId = new Guid();
        var result = await controller.DeleteCategoryLocale(unrealId, cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }
    [Fact]
    public async Task DeleteCategoryLocale_SholudReturnNoContent_WhenArticleContainsInDb()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.CategoryLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<CategoryLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new CategoryLocale());

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var idOfCategoryWhichContain = new Guid();
        var result = await controller.DeleteCategoryLocale(idOfCategoryWhichContain, cultureId) as NoContentResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NoContent);
        _repositoryManager.CategoryLocales.ReceivedCalls().Should().HaveCount(2);
    }

    [Fact]
    public async Task UpdateCategoryLocale_ShouldReturnBadRequestAndLogging_WhenRecieveNull()
    {
        //Arrange
        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.UpdateCategoryLocale(new Guid(), null, Guid.Empty);
        var statusCode = (result as BadRequestObjectResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.BadRequest);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }
    [Fact]
    public async Task UpdateCategoryLocale_ShouldReturnNotFoundAndLogging_WhenCultureDontExist()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var result = await controller.UpdateCategoryLocale(new Guid(), new CategoryLocaleToUpdateDto(), cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }
    [Fact]
    public async Task UpdateCategoryLocale_ShouldReturnNotFoundAndLogging_WhenCategoryRecievedIdWhichNotContainInDb()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.CategoryLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<CategoryLocale, bool>>>(), Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unCorrectId = new Guid();
        var result = await controller.UpdateCategoryLocale(unCorrectId, new CategoryLocaleToUpdateDto(), cultureId) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }
    [Fact]
    public async Task UpdateCategoryLocale_ShouldReturnNoContent_WhenAllIsGood()
    {
        //Arrange
        Guid cultureId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        _repositoryManager.Cultures.GetCultureAsync(cultureId, Arg.Any<ChangesType>())
            .Returns(new Culture());

        _repositoryManager.CategoryLocales
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<CategoryLocale, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new CategoryLocale());

        var controller = new CategoryLocaleController(_repositoryManager, _mapper, _logger, _messageProvider);

        //Act
        var unCorrectId = new Guid();
        var result = await controller.UpdateCategoryLocale(unCorrectId, new CategoryLocaleToUpdateDto(), cultureId);
        var statusCode = (result as NoContentResult)!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NoContent);
        _mapper.ReceivedCalls().Should().HaveCount(1);
        _repositoryManager.CategoryLocales.ReceivedCalls().Should().HaveCount(1);
    }
}