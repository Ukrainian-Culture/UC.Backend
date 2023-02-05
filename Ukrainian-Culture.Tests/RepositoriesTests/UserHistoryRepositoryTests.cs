namespace Ukrainian_Culture.Tests.RepositoriesTests;

public class UserHistoryRepositoryTests
{
    private readonly RepositoryContext _context;

    public UserHistoryRepositoryTests()
    {
        using var factory = new ConnectionFactory();
        _context = factory.CreateContextForInMemory(new UserHistoryModel());
    }

    [Fact]
    public async Task
        GetAllUserHistoryByConditionAsync_ShouldReturnAllCollection_WhenExpressionIsEqualToTrueAndDbIsNotEmpty()
    {
        //Arrange
        Guid firstUserHistoryId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid secondUserHistoryId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");

        _context.UsersHistories.AddRange(new List<UserHistory>()
            {
                new()
                {
                    Id = firstUserHistoryId,
                },
                new()
                {
                    Id = secondUserHistoryId,
                }
            }
        );

        await _context.SaveChangesAsync();

        var userHistoryRepository = new UserHistoryRepository(_context);

        //Act
        var userHistory
            = (await userHistoryRepository.GetAllUserHistoryByConditionAsync(_ => true, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        userHistory.Should().HaveCount(2);
        userHistory[0].Id.Should().Be(firstUserHistoryId);
        userHistory[1].Id.Should().Be(secondUserHistoryId);
    }

    [Theory]
    [InlineData("5eca5808-4f44-4c4c-b481-72d2bdf24203")]
    [InlineData("5b32effd-1111-4cab-8ac9-3258c746aa53")]
    public async Task
        GetAllUserHistoryByConditionAsync_ShouldReturnPartOfCollection_WhenHasCorrectExpressionAndDbIsNotEmpty(
            string idToCompareAsStr)
    {
        //Arrange
        var idToCompare = new Guid(idToCompareAsStr);
        Guid firstUserHistoryId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid secondUserHistoryId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");

        _context.UsersHistories.AddRange(new List<UserHistory>
            {
                new() { Id = firstUserHistoryId },
                new() { Id = secondUserHistoryId }
            }
        );
        await _context.SaveChangesAsync();
        var userHistoryRepository = new UserHistoryRepository(_context);

        //Act
        var userHistory
            = (await userHistoryRepository.GetAllUserHistoryByConditionAsync(art => art.Id == idToCompare,
                ChangesType.AsNoTracking))
            .ToList();

        //Assert
        userHistory.Should().HaveCount(1);
        userHistory[0].Id.Should().Be(idToCompare);
    }

    [Fact]
    public async Task GetAllUserHistoryByConditionAsync_ShouldReturnEmptyCollection_WhenDbIsEmpty()
    {
        //Arrange
        var userHistoryRepository = new UserHistoryRepository(_context);

        //Act
        var userHistory
            = (await userHistoryRepository.GetAllUserHistoryByConditionAsync(_ => true, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        userHistory.Should().BeEmpty();
    }

    [Fact]
    public async Task
        GetAllUserHistoryByConditionAsync_ShouldReturnEmptyCollection_WhenExpressionIsEqualToFalseAndDbIsNotEmpty()
    {
        //Arrange
        Guid firstUserHistoryId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
        Guid secondUserHistoryId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");

        _context.UsersHistories.AddRange(new List<UserHistory>
            {
                new() { Id = firstUserHistoryId },
                new() { Id = secondUserHistoryId }
            }
        );
        await _context.SaveChangesAsync();
        var userHistoryRepository = new UserHistoryRepository(_context);

        //Act
        var userHistory
            = (await userHistoryRepository.GetAllUserHistoryByConditionAsync(_ => false, ChangesType.AsNoTracking))
            .ToList();

        //Assert
        userHistory.Should().BeEmpty();
    }

    [Fact]
    public async Task
        GetAllUserHistoryByConditionAsync_ShouldReturnEmptyCollection_WhenExpressionIsUncorrectAndDbIsNotEmpty()
    {
        //Arrange
        Guid firstUserHistoryId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");

        _context.UsersHistories.AddRange(new List<UserHistory>()
            {
                new() { Id = firstUserHistoryId }
            }
        );
        await _context.SaveChangesAsync();
        var userHistoryRepository = new UserHistoryRepository(_context);

        //Act
        Guid nonExistedId = new("5b32effd-1111-4cab-8ac9-3258c746aa53");
        var userHistory
            = (await userHistoryRepository.GetAllUserHistoryByConditionAsync(art => art.Id == nonExistedId,
                ChangesType.AsNoTracking))
            .ToList();

        //Assert
        userHistory.Should().BeEmpty();
    }

    [Fact]
    public async Task AddHistoryToUser_ShouldAddHistoryToUser_WhenUserExistAndHistoryIsCorrect()
    {
        //Arrange
        var userId = new Guid("a706959a-6eef-4ea5-ba6c-79844446f950");
        await _context.Users.AddAsync(new User
        {
            Id = userId
        });
        await _context.SaveChangesAsync();
        var repository = new UserHistoryRepository(_context);

        //Act
        repository.AddHistoryToUser(userId, new UserHistory());
        await _context.SaveChangesAsync();

        var result = await _context
            .Users
            .AsNoTracking()
            .Include(user => user.History)
            .FirstOrDefaultAsync(user => user.Id == userId);

        //Assert
        result.Should().NotBeNull();
        result!.History.Should().HaveCount(1);
        _context.UsersHistories.Should().HaveCount(1);
    }

    [Fact]
    public async Task AddHistoryToUser_ShouldThrowException_WhenRecieveNull()
    {
        //Arrange
        var userId = new Guid("a706959a-6eef-4ea5-ba6c-79844446f950");
        var repository = new UserHistoryRepository(_context);

        try
        {
            //Act
            repository.AddHistoryToUser(userId, null);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<NullReferenceException>();
        }
    }
}