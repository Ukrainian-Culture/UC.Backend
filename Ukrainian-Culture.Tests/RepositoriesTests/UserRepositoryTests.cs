namespace Ukrainian_Culture.Tests.RepositoriesTests;

public class UserRepositoryTests
{
    private readonly RepositoryContext _context;
    private readonly Guid _firstId = new("eff42197-94d2-4e2a-b5cf-bcb60082858d");
    private readonly Guid _secondId = new("f4d8a14c-8f6f-4f22-b641-1c6b627a9482");

    public UserRepositoryTests()
    {
        using var factory = new ConnectionFactory();
        _context = factory.CreateContextForInMemory(new UserModel());
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnListOfUsers_WhenDbIsNotEmpty()
    {
        //Arrange
        _context.Users.AddRange(new List<User>
        {
            new()
            {
                Id = _firstId
            },
            new()
            {
                Id = _secondId
            }
        });
        await _context.SaveChangesAsync();
        var userRepository = new UserRepository(_context);

        //Act
        var users = (await userRepository.GetAllUsersAsync(ChangesType.AsNoTracking)).ToList();

        //Assert
        users.Count.Should().Be(2);
        users[0].Id.Should().Be(_firstId);
        users[1].Id.Should().Be(_secondId);
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnEmpty_WhenDbIsEmpty()
    {
        //Arrange
        var userRepository = new UserRepository(_context);

        //Act
        var users = (await userRepository.GetAllUsersAsync(ChangesType.AsNoTracking)).ToList();

        //Assert
        users.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenIdContainsInDb()
    {
        //Arrange
        _context.Users.AddRange(new List<User>
        {
            new()
            {
                Id = _firstId
            },
            new()
            {
                Id = _secondId
            }
        });
        await _context.SaveChangesAsync();
        var userRepository = new UserRepository(_context);

        //Act
        var user = await userRepository.GetFirstByConditionAsync(user => user.Id == _firstId, ChangesType.AsNoTracking);

        //Assert
        user.Id.Should().Be(_firstId);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldThrowException_WhenIdDoesNotContainInDb()
    {
        //Arrange
        var userRepository = new UserRepository(_context);
        try
        {
            //Act
            var user = await userRepository.GetFirstByConditionAsync(user => user.Id == _firstId,
                ChangesType.AsNoTracking);
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }
    }

    [Fact]
    public async Task CreateUser_ShouldCreateNewUserInDb_WhenCorrectData()
    {
        //Arrange
        var userRepository = new UserRepository(_context);

        //Act
        userRepository.CreateUser(new User
        {
            Id = _firstId
        });
        await _context.SaveChangesAsync();

        //Assert
        _context.Users.Should().HaveCount(1);
    }

    [Fact]
    public async Task CreateUser_ShouldThrowException_WhenMissingData()
    {
        //Arrange
        var userRepository = new UserRepository(_context);

        try
        {
            //Act
            userRepository.CreateUser(new User
            {
                Id = _firstId
            });
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<DbUpdateException>();
        }
    }

    [Fact]
    public async Task CreateUser_ShouldThrowException_WhenIdAlreadyExists()
    {
        //Arrange
        _context.Users.AddRange(new List<User>
        {
            new()
            {
                Id = _firstId
            }
        });
        await _context.SaveChangesAsync();
        var userRepository = new UserRepository(_context);

        try
        {
            //Act
            userRepository.CreateUser(new User
            {
                Id = _firstId
            });
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }
    }

    [Fact]
    public async Task UpdateUser_ShouldUpdateUser_WhenNewDataIsCorrect()
    {
        //Arrange
        var user = new User
        {
            Id = _firstId,
            UserName = "Test"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var userRepository = new UserRepository(_context);

        //Act
        user.UserName = "new name";
        userRepository.UpdateUser(user);
        await _context.SaveChangesAsync();

        //Assert
        (await _context.Users.FirstAsync(use => use.Id == user.Id)).UserName.Should().Be("new name");
    }

    [Fact]
    public async Task UpdateUser_ShouldThrowException_WhenTryToModifyId()
    {
        //Arrange
        var user = new User
        {
            Id = _firstId
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var userRepository = new UserRepository(_context);

        try
        {
            //Act
            user.Id = _secondId;
            userRepository.UpdateUser(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<InvalidOperationException>();
        }
    }


    [Fact]
    public async Task DeleteUser_ShoulDeleteUser_WhenUserContainInDb()
    {
        //Arrange
        var user = new User
        {
            Id = _firstId
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var userRepository = new UserRepository(_context);

        //Act
        userRepository.DeleteUser(user);
        await _context.SaveChangesAsync();

        //Assert
        _context.Users.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteUser_ShoulThrowException_WhenTryToDeleteUnrealUser()
    {
        //Arrange
        _context.Users.Add(new User
        {
            Id = _secondId
        });
        await _context.SaveChangesAsync();
        var userRepository = new UserRepository(_context);

        try
        {
            //Act
            var user = new User
            {
                Id = _firstId
            };
            userRepository.DeleteUser(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<DbUpdateConcurrencyException>();
        }
    }

    [Fact]
    public async Task DeleteUser_ShoulThrowException_WhenUserNoContainInDb()
    {
        //Arrange
        var userRepository = new UserRepository(_context);

        try
        {
            //Act
            var user = new User
            {
                Id = _firstId
            };
            userRepository.DeleteUser(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            //Assert
            ex.Should().BeOfType<DbUpdateConcurrencyException>();
        }
    }
}