using Microsoft.AspNetCore.Identity;

namespace Ukrainian_Culture.Tests.ControllersTests;

public class AccountControllerTests
{
    private readonly IAccountRepository _account= Substitute.For<IAccountRepository>();

    [Fact]
    public async Task SignUp_ShouldReturnOkStatus_WhenUserDataIsCorrect()
    {
        //arrange
        _account.SignUpAsync(Arg.Any<SignUpUser>(), "User").Returns(IdentityResult.Success);
        var controller =new AccountController(_account);
        string role = "User";
        var user = new SignUpUser()
        {
            FirstName = "Name1",
            LastName = "Surname1",
            Email= "Name1@gmail.com",
            Password="TTCGCghcvhj",
            ConfirmPassword="TTCGCghcvhj"
        };
        //act
        var result = await controller.SignUp(user, role) as OkObjectResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task SignUp_ShouldReturnNull_WhenInvalidPassword()
    {
        //arrange
        _account.SignUpAsync(Arg.Any<SignUpUser>(), "User").Returns(IdentityResult.Failed());
        var controller = new AccountController(_account);
        string role = "User";
        var user = new SignUpUser()
        {
            FirstName = "Name1",
            LastName = "Surname1",
            Email = "Name1@gmail.com",
            Password = "TTCGCghcvhj",
            ConfirmPassword = "TTCGC"
        };

        //act
        var result = await controller.SignUp(user, role) as OkObjectResult;

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
        string expected ="";
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
}
