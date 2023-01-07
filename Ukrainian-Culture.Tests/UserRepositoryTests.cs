//using System.Runtime.CompilerServices;
//using Contracts;
//using Entities;
//using Entities.Models;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
//using Repositories;
//using Ukrainian_Culture.Tests.DbModels;

//namespace Ukrainian_Culture.Tests;

//public class UserRepositoryTests
//{
//    private readonly RepositoryContext _context;
//    public UserRepositoryTests()
//    {
//        using var factory = new ConnectionFactory();
//        _context = factory.CreateContextForInMemory(new UserModel());
//    }

//    [Fact]
//    public async Task GetAllUsersAsync_ShouldReturnListOfUsers_WhenDbIsNotEmpty()
//    {
//        //Arrange
//        _context.Users.AddRange(new List<User>
//        {
//            new()
//            {
//                Id = 1,
//                Name = "1",
//                Login = "1",
//                Phone = "1"
//            },
//            new()
//            {
//                Id = 2,
//                Login = "2",
//                Name = "2",
//                Phone = "2"
//            }
//        });
//        await _context.SaveChangesAsync();
//        var userRepository = new UserRepository(_context);

//        //Act
//        var users = (await userRepository.GetAllUsersAsync(ChangesType.AsNoTracking)).ToList();

//        //Assert
//        users.Count.Should().Be(2);
//        users[0].Id.Should().Be(1);
//        users[1].Id.Should().Be(2);
//    }

//    [Fact]
//    public async Task GetAllUsersAsync_ShouldReturnEmpty_WhenDbIsEmpty()
//    {
//        //Arrange
//        var userRepository = new UserRepository(_context);

//        //Act
//        var users = (await userRepository.GetAllUsersAsync(ChangesType.AsNoTracking)).ToList();

//        //Assert
//        users.Should().BeEmpty();
//    }

//    [Fact]
//    public async Task GetUserByIdAsync_ShouldReturnUser_WhenIdContainsInDb()
//    {
//        //Arrange
//        _context.Users.AddRange(new List<User>
//        {
//            new()
//            {
//                Id = 1,
//                Name = "1",
//                Login = "1",
//                Phone = "1"
//            },
//            new()
//            {
//                Id = 2,
//                Login = "2",
//                Name = "2",
//                Phone = "2"
//            }
//        });
//        await _context.SaveChangesAsync();
//        var userRepository = new UserRepository(_context);

//        //Act
//        var user = await userRepository.GetUserByIdAsync(1, ChangesType.AsNoTracking);

//        //Assert
//        user.Id.Should().Be(1);
//        user.Name.Should().Be("1");
//        user.Phone.Should().Be("1");
//    }

//    [Fact]
//    public async Task GetUserByIdAsync_ShouldThrowException_WhenIdDoesNotContainInDb()
//    {
//        //Arrange
//        var userRepository = new UserRepository(_context);
//        try
//        {
//            //Act
//            var user = await userRepository.GetUserByIdAsync(1, ChangesType.AsNoTracking);
//        }
//        catch (Exception ex)
//        {
//            //Assert
//            ex.Should().BeOfType<InvalidOperationException>();
//        }
//    }

//    [Fact]
//    public async Task CreateUser_ShouldCreateNewUserInDb_WhenCorrectData()
//    {
//        //Arrange
//        var userRepository = new UserRepository(_context);

//        //Act
//        userRepository.CreateUser(new User
//        {
//            Id = 1,
//            Name = "1",
//            Login = "1",
//            Phone = "1"
//        });
//        await _context.SaveChangesAsync();

//        //Assert
//        _context.Users.Should().HaveCount(1);
//    }
//    [Fact]
//    public async Task CreateUser_ShouldThrowException_WhenMissingData()
//    {
//        //Arrange
//        var userRepository = new UserRepository(_context);

//        try
//        {
//            //Act
//            userRepository.CreateUser(new User
//            {
//                Id = 1,
//                Login = "1",
//                Phone = "1"
//            });
//            await _context.SaveChangesAsync();
//        }
//        catch (Exception ex)
//        {
//            //Assert
//            ex.Should().BeOfType<DbUpdateException>();
//        }
//    }
//    [Fact]
//    public async Task CreateUser_ShouldThrowException_WhenIdAlreadyExists()
//    {
//        //Arrange
//        _context.Users.AddRange(new List<User>
//        {
//            new()
//            {
//                Id = 1,
//                Name = "1",
//                Login = "1",
//                Phone = "1"
//            }
//        });
//        await _context.SaveChangesAsync();
//        var userRepository = new UserRepository(_context);

//        try
//        {
//            //Act
//            userRepository.CreateUser(new User
//            {
//                Id = 1,
//                Name = "1",
//                Login = "1",
//                Phone = "1"
//            });
//            await _context.SaveChangesAsync();
//        }
//        catch (Exception ex)
//        {
//            //Assert
//            ex.Should().BeOfType<InvalidOperationException>();
//        }
//    }

//    [Fact]
//    public async Task UpdateUser_ShouldUpdateUser_WhenNewDataIsCorrect()
//    {

//        //Arrange
//        var user = new User
//        {
//            Id = 1,
//            Name = "1",
//            Login = "1",
//            Phone = "1"
//        };
//        _context.Users.Add(user);
//        await _context.SaveChangesAsync();
//        var userRepository = new UserRepository(_context);

//        //Act
//        user.Name = "new name";
//        userRepository.UpdateUser(user);
//        await _context.SaveChangesAsync();

//        //Assert
//        (await _context.Users.FirstAsync(use => use.Id == user.Id)).Name.Should().Be("new name");
//    }

//    [Fact]
//    public async Task UpdateUser_ShouldThrowException_WhenTryToModifyId()
//    {

//        //Arrange
//        var user = new User
//        {
//            Id = 1,
//            Name = "1",
//            Login = "1",
//            Phone = "1"
//        };
//        _context.Users.Add(user);
//        await _context.SaveChangesAsync();
//        var userRepository = new UserRepository(_context);

//        try
//        {
//            //Act
//            user.Id = 2;
//            userRepository.UpdateUser(user);
//            await _context.SaveChangesAsync();
//        }
//        catch (Exception ex)
//        {
//            //Assert
//            ex.Should().BeOfType<InvalidOperationException>();
//        }
//    }


//    [Fact]
//    public async Task DeleteUser_ShoulDeleteUser_WhenUserContainInDb()
//    {
//        //Arrange
//        var user = new User
//        {
//            Id = 1,
//            Name = "1",
//            Login = "1",
//            Phone = "1"
//        };
//        _context.Users.Add(user);
//        await _context.SaveChangesAsync();
//        var userRepository = new UserRepository(_context);

//        //Act
//        userRepository.DeleteUser(user);
//        await _context.SaveChangesAsync();

//        //Assert
//        _context.Users.Should().BeEmpty();
//    }
//    [Fact]
//    public async Task DeleteUser_ShoulThrowException_WhenTryToDeleteUnrealUser()
//    {
//        //Arrange
//        _context.Users.Add(new User
//        {
//            Id = 2,
//            Name = "2",
//            Login = "2",
//            Phone = "2"
//        });
//        await _context.SaveChangesAsync();
//        var userRepository = new UserRepository(_context);

//        try
//        {
//            //Act
//            var user = new User
//            {
//                Id = 1,
//                Name = "1",
//                Login = "1",
//                Phone = "1"
//            };
//            userRepository.DeleteUser(user);
//            await _context.SaveChangesAsync();
//        }
//        catch (Exception ex)
//        {
//            //Assert
//            ex.Should().BeOfType<DbUpdateConcurrencyException>();
//        }
//    }

//    [Fact]
//    public async Task DeleteUser_ShoulThrowException_WhenUserNoContainInDb()
//    {
//        //Arrange
//        var userRepository = new UserRepository(_context);

//        try
//        {
//            //Act
//            var user = new User
//            {
//                Id = 1,
//                Name = "1",
//                Login = "1",
//                Phone = "1"
//            };
//            userRepository.DeleteUser(user);
//            await _context.SaveChangesAsync();
//        }
//        catch (Exception ex)
//        {
//            //Assert
//            ex.Should().BeOfType<DbUpdateConcurrencyException>();
//        }
//    }
//}