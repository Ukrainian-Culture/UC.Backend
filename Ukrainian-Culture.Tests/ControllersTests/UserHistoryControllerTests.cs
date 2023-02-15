namespace Ukrainian_Culture.Tests.ControllersTests;

public class UserHistoryControllerTests
{
    private readonly IRepositoryManager _repositoryManager = Substitute.For<IRepositoryManager>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly ILoggerManager _logger = Substitute.For<ILoggerManager>();
    private readonly IErrorMessageProvider _errorMessageProvider = Substitute.For<IErrorMessageProvider>();

    [Fact]
    public async Task GetAllUserHistory_ShouldReturnNotFoundAndLog_WhenUserDontExist()
    {
        //Arrange
        const string email = "test@gmail.com";
        TestableUserFromDb().ReturnsNull();
        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);

        //Act
        var result = await controller.GetAllUserHistory(email) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAllUserHistory_ShouldReturnOkResult_WhenUserExistAndIdIsCorrect()
    {
        //Arrange
        const string email = "test@gmail.com";
        TestableUserFromDb().Returns(new User());

        _repositoryManager
            .UserHistory
            .GetAllUserHistoryByConditionAsync(Arg.Any<Expression<Func<UserHistory, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<UserHistory>());
        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);

        //Act
        var result = await controller.GetAllUserHistory(email) as OkObjectResult;
        var status = result!.StatusCode;

        //Assert
        status.Should().Be((int)HttpStatusCode.OK);
        _repositoryManager.UserHistory.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task AddHistoryToUser_ShouldReturnBadResultAndLog_WhenRecieveNull()
    {
        //Arrange
        const string email = "test@gmail.com";
        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);
        //Act
        var result = await controller.AddHistoryToUser(email, null) as BadRequestObjectResult;
        var statusCode = result!.StatusCode;
        //Assert
        statusCode.Should().Be((int)HttpStatusCode.BadRequest);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task AddHistoryToUser_ShouldReturnNotFoundAndLog_WhenUserEntityDoesntExist()
    {
        //Arrange
        const string email = "test@gmail.com";
        TestableUserFromDb().ReturnsNull();

        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);
        //Act
        var result = await controller.AddHistoryToUser(email, new HistoryToCreateDto()) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;
        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }
    
    [Fact]
    public async Task AddHistoryToUser_ShouldReturnNoContent_WhenUserExistInDbAndRecieveCorrectObj()
    {
        //Arrange
        const string email = "test@gmail.com";
        TestableUserFromDb().Returns(new User());
        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);
        //Act
        var result = await controller.AddHistoryToUser(email, new HistoryToCreateDto()) as NoContentResult;
        var status = result!.StatusCode;
        //Assert
        status.Should().Be((int)HttpStatusCode.NoContent);
        _mapper.ReceivedCalls().Should().HaveCount(1);
        _repositoryManager.UserHistory.ReceivedCalls().Should().HaveCount(1);
    }
    
    private Task<User?> TestableUserFromDb()
    {
        return _repositoryManager
            .Users
            .GetFirstByConditionAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<ChangesType>());
    }
}