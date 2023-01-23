using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Ukrainian_Culture.Tests.ControllersTests;

public class AccountControllerTests
{
    private readonly IAccountRepository _account = Substitute.For<IAccountRepository>();

    [Fact]
    public async Task SignUp_ShouldReturnOkStatus_WhenUserDataIsCorrect()
    {
        //arrange
        _account.SignUpAsync(Arg.Any<SignUpUser>(), Arg.Any<HttpContext>(), Arg.Any<UrlHelper>()).Returns(IdentityResult.Success);
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
        _account.SignUpAsync(Arg.Any<SignUpUser>(), Arg.Any<HttpContext>(), Arg.Any<UrlHelper>()).Returns(IdentityResult.Failed());
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
    public async Task ConfirmEmailAsync_ShouldReturnFalse_WhenUserDoesNotExistsOrCodeIsInvalid()
    {
        //arrange
        bool expected = false;
        _account.ConfirmEmailAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(expected);
        var controller = new AccountController(_account);
        var userId = "123";
        var code = "CfDJ8KMcWVA73Z9EpmagZUfXsP6OjqaQf8JHGuWfQ%2Brv%2FBLqADK4CvEIRJk0lx1i5t8rLXddkxN%2BQEUqooEAHqAo4a50TLPKiAbLSev4WzlEJywh39RoaDH04EfuIPfvL2IG2kQZEtSNYv5M4%2FSEbnDbzyya0s8ScLHMrg%2BOnG31wXpqGTC1rfHmsFn8gfflgknmVqcuUUiYNi3velL7vLMYf91%2B%2F7wFhQZvwljdkXpQ5g%2Fxx%2FOGZBonNraB5mFOXIy3Qw%3D%3D";
        //act
        var result = await controller.ConfirmEmail(userId, code) as OkObjectResult;
        
        //assert
        result.Equals(expected);
    }
    public async Task ConfirmEmailAsync_ShouldReturnTrue_WhenEmailWasConfirmed()
    {
        //arrange
        bool expected = true;
        _account.ConfirmEmailAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(expected);
        var controller = new AccountController(_account);
        var userId = "5ba6d703-fa9e-4771-561e-08dafcd8b602";
        var code = "CfDJ8KMcWVA73Z9EpmagZUfXsP6OjqaQf8JHGuWfQ%2Brv%2FBLqADK4CvEIRJk0lx1i5t8rLXddkxN%2BQEUqooEAHqAo4a50TLPKiAbLSev4WzlEJywh39RoaDH04EfuIPfvL2IG2kQZEtSNYv5M4%2FSEbnDbzyya0s8ScLHMrg%2BOnG31wXpqGTC1rfHmsFn8gfflgknmVqcuUUiYNi3velL7vLMYf91%2B%2F7wFhQZvwljdkXpQ5g%2Fxx%2FOGZBonNraB5mFOXIy3Qw%3D%3D";
        //act
        var result = await controller.ConfirmEmail(userId, code) as OkObjectResult;

        //assert
        result.Equals(expected);
    }
}
