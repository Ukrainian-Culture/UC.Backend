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
        var id = new Guid("1bd9644b-7a67-44f0-8e2b-1bc4ac8dc920");
        _repositoryManager
            .Users
            .GetFirstByConditionAsync(user => user.Id == id, Arg.Any<ChangesType>())
            .ReturnsNull();
        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);

        //Act
        var result = await controller.GetAllUserHistory(id) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;

        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAllUserHistory_ShouldReturnOkResult_WhenUserExistAndIdIsCorrect()
    {
        //Arrange
        var id = new Guid("1bd9644b-7a67-44f0-8e2b-1bc4ac8dc920");
        _repositoryManager.Users.GetFirstByConditionAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new User());

        _repositoryManager
            .UserHistory
            .GetAllUserHistoryByConditionAsync(Arg.Any<Expression<Func<UserHistory, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new List<UserHistory>());
        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);

        //Act
        var result = await controller.GetAllUserHistory(id) as OkObjectResult;
        var status = result!.StatusCode;

        //Assert
        status.Should().Be((int)HttpStatusCode.OK);
        _repositoryManager.UserHistory.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task AddHistoryToUser_ShouldReturnBadResultAndLog_WhenRecieveNull()
    {
        //Arrange
        var id = new Guid("1bd9644b-7a67-44f0-8e2b-1bc4ac8dc920");
        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);
        //Act
        var result = await controller.AddHistoryToUser(id, null) as BadRequestObjectResult;
        var statusCode = result!.StatusCode;
        //Assert
        statusCode.Should().Be((int)HttpStatusCode.BadRequest);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task AddHistoryToUser_ShouldReturnNotFoundAndLog_WhenUserEntityDoesntExist()
    {
        //Arrange
        var id = new Guid("1bd9644b-7a67-44f0-8e2b-1bc4ac8dc920");
        _repositoryManager.Users.GetFirstByConditionAsync(user => user.Id == id, Arg.Any<ChangesType>())
            .ReturnsNull();

        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);
        //Act
        var result = await controller.AddHistoryToUser(id, new HistoryToCreateDto()) as NotFoundObjectResult;
        var statusCode = result!.StatusCode;
        //Assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
        _logger.ReceivedCalls().Should().HaveCount(1);
    }

    [Fact]
    public async Task AddHistoryToUser_ShouldReturnNoContent_WhenUserExistInDbAndRecieveCorrectObj()
    {
        //Arrange
        var id = new Guid("1bd9644b-7a67-44f0-8e2b-1bc4ac8dc920");
        _repositoryManager.Users.GetFirstByConditionAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<ChangesType>())
            .Returns(new User());
        var controller = new UserHistoryController(_repositoryManager, _mapper, _logger, _errorMessageProvider);
        //Act
        var result = await controller.AddHistoryToUser(id, new HistoryToCreateDto()) as NoContentResult;
        var status = result!.StatusCode;
        //Assert
        status.Should().Be((int)HttpStatusCode.NoContent);
        _mapper.ReceivedCalls().Should().HaveCount(1);
        _repositoryManager.UserHistory.ReceivedCalls().Should().HaveCount(1);
    }
}