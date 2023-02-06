
using Microsoft.AspNetCore.Http;

namespace Ukrainian_Culture.Tests.ControllersTests;

public class AccountControllerTests
{
    private readonly IAccountRepository _account = Substitute.For<IAccountRepository>();

    [Fact]
    public async Task SignUp_ShouldReturnOkStatus_WhenUserDataIsCorrect()
    {
        //arrange
        _account.SignUpAsync(Arg.Any<SignUpUser>()).Returns(IdentityResult.Success);
        var controller = new AccountController(_account);
        var user = new SignUpUser()
        {
            FirstName = "Name1",
            LastName = "Surname1",
            Email = "Name1@gmail.com",
            Password = "TTCGCghcvhj",
            ConfirmPassword = "TTCGCghcvhj"
        };
        //act
        var result = await controller.SignUp(user) as OkObjectResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task SignUp_ShouldReturnNull_WhenInvalidPassword()
    {
        //arrange
        _account.SignUpAsync(Arg.Any<SignUpUser>()).Returns(IdentityResult.Failed());
        var controller = new AccountController(_account);
        var user = new SignUpUser()
        {
            FirstName = "Name1",
            LastName = "Surname1",
            Email = "Name1@gmail.com",
            Password = "TTCGCghcvhj",
            ConfirmPassword = "TTCGC"
        };

        //act
        var result = await controller.SignUp(user) as OkObjectResult;

        //assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Login_ShouldReturnStringToken_WhenUserDataIsCorrect()
    {
        //arrange
        string expected = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVm9sb2R5YTIyQGdtYWlsLmNvbSIsImp0aSI6IjgwZGJiN2E0LWE0MzktNGZiYi1iNWYxLTA4ODdiMTY1ODBlNSIsImV4cCI6MTY3MzM1ODEwOSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDEiLCJhdWQiOiJVc2VyIn0.7g5ajeanSgyojTqBmQ6PqcUhjzx0V2zp3xGcec_4vbg";
        _account.LoginAsync(Arg.Any<SignInUser>()).Returns(expected);
        var controller = new AccountController(_account);
        var user = new SignInUser()
        {
            FirstName = "Volodya22",
            Email = "Volodya22@gmail.com",
            Password = "TTCGCghcvhj"
        };

        //act
        var result = await controller.Login(user) as OkObjectResult;

        //assert
        result.Equals(expected);
    }
    [Fact]
    public async Task Login_ShouldReturnEmptyStringToken_WhenInvalidPassword()
    {
        //arrange
        string expected = "";
        _account.LoginAsync(Arg.Any<SignInUser>()).Returns(expected);
        var controller = new AccountController(_account);
        var user = new SignInUser()
        {
            FirstName = "Volodya22",
            Email = "Volodya22@gmail.com",
            Password = "T"
        };

        //act
        var result = await controller.Login(user) as OkObjectResult;

        //assert
        result.Should().BeNull();
    }

    [Fact]

    public async Task ChangePassword_ShouldReturnStatusOk_WhenChangePasswordDtoCorrect()
    {
        //arrange
        _account.ChangePasswordAsync(Arg.Any<ChangePasswordDto>()).Returns(IdentityResult.Success);
        var controller = new AccountController(_account);
        var user = new ChangePasswordDto()
        {
            Email = "Volodya22@gmail.com",
            CurrentPassword = "12345678",
            NewPassword = "87654321",
            ConfirmPassword = "87654321"
        };

        //act
        var result = await controller.ChangePassword(user) as OkObjectResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnNotFound_WhenUserNotFound()
    {
        //arrange
        _account.ChangePasswordAsync(Arg.Any<ChangePasswordDto>()).Returns(IdentityResult.Failed());
        var controller = new AccountController(_account);
        var user = new ChangePasswordDto()
        {
            Email = "",
            CurrentPassword = "12345678",
            NewPassword = "87654321",
            ConfirmPassword = "87654321"
        };

        //act
        var result = await controller.ChangePassword(user);
        var statusCode = (result as NotFoundResult)!.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ChangeFirstName_ShouldReturnOkStatus_WhenUserAreValid()
    {
        //arrange
        _account.ChangeFirstNameAsync(Arg.Any<ChangeFirstNameDto>()).Returns(IdentityResult.Success);
        var controller = new AccountController(_account);
        var user = new ChangeFirstNameDto()
        {
            Email = "Volodya22@gmail.com",
            NewFirstName = "Vova"
        };

        //act
        var result = await controller.ChangeFirstName(user) as OkObjectResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task ChangeFirstName_ShouldReturnNotFound_WhenEmailEmpty()
    {
        //arrange
        _account.ChangeFirstNameAsync(Arg.Any<ChangeFirstNameDto>()).Returns(IdentityResult.Failed());
        var controller = new AccountController(_account);
        var user = new ChangeFirstNameDto()
        {
            Email = "",
            NewFirstName = "Vova"
        };

        //act
        var result = await controller.ChangeFirstName(user);
        var statusCode = (result as NotFoundResult)!.StatusCode;


        //assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ChangeLastName_ShouldReturnOkStatus_WhenChangeLastNameDtoValid()
    {
        //arrange
        _account.ChangeLastNameAsync(Arg.Any<ChangeLastNameDto>()).Returns(IdentityResult.Success);
        var controller = new AccountController(_account);
        var user = new ChangeLastNameDto()
        {
            Email = "Name1@gmail.com",
            NewLastName = "Surname2"
        };

        //act
        var result = await controller.ChangeLastName(user) as OkObjectResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task ChangeLastName_ShouldReturnNotFound_WhenChangeLastNameDtoInValid()
    {
        //arrange
        _account.ChangeLastNameAsync(Arg.Any<ChangeLastNameDto>()).Returns(IdentityResult.Failed());
        var controller = new AccountController(_account);
        var user = new ChangeLastNameDto()
        {
            Email = "Name1@gmail.com",
            NewLastName = "Surname2"
        };

        //act
        var result = await controller.ChangeLastName(user);
        var statusCode = (result as NotFoundResult)!.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ChangeEmail_ShouldReturnOkStatus_WhenChangeEmailDtoValid()
    {
        //arrange
        _account.ChangeEmailAsync(Arg.Any<ChangeEmailDto>()).Returns(IdentityResult.Success);
        var controller = new AccountController(_account);
        var user = new ChangeEmailDto()
        {
            CurrentEmail = "user@gmail.com",
            NewEmail = "user1@gmail.com"
        };

        //act
        var result = await controller.ChangeEmail(user) as OkObjectResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task ChangeEmail_ShouldReturnNotFound_WhenChangeEmailDtoInValid()
    {
        //arrange
        _account.ChangeEmailAsync(Arg.Any<ChangeEmailDto>()).Returns(IdentityResult.Failed());
        var controller = new AccountController(_account);
        var user = new ChangeEmailDto()
        {
            CurrentEmail = "usergmail.com",
            NewEmail = "user1@gmail.com"
        };

        //act
        var result = await controller.ChangeEmail(user);
        var statusCode = (result as NotFoundResult)!.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
    }



    [Fact]
    public async Task DeleteAccount_ShouldReturnNotFound_WhenUserDoesntExist()
    {
        //arrange
        var id = new Guid();
        _account.DeleteAccountAsync(id).Returns(IdentityResult.Failed());
        var controller = new AccountController(_account);

        //act
        var result = await controller.DeleteAccount(id);
        var statusCode = (result as NotFoundResult)!.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteAccount_ShouldReturnOkStatus_WhenIdValid()
    {
        //arrange
        var id = new Guid();
        _account.DeleteAccountAsync(id).Returns(IdentityResult.Success);
        var controller = new AccountController(_account);

        //act
        var result = await controller.DeleteAccount(id) as OkObjectResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);

    }
}
