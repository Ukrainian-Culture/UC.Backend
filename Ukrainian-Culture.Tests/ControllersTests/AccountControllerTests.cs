
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
        var user = new SignUpUser
        {
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
        var user = new SignUpUser
        {
            Email = "Name1@gmail.com",
            Password = "TTC",
            ConfirmPassword = "TTCGCghcvhj"
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
        _account.LoginAsync(Arg.Any<SignInUser>()).Returns(new TokenModel());
        var controller = new AccountController(_account);
        var user = new SignInUser()
        {
            Email = "Volodya22@gmail.com",
            Password = "TTCGCghcvhj"
        };
        //act
        var result = await controller.Login(user) as OkObjectResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task Login_ShouldReturnEmptyStringToken_WhenInvalidPassword()
    {
        //arrange
        _account.LoginAsync(Arg.Any<SignInUser>()).ReturnsNull();
        var controller = new AccountController(_account);
        var user = new SignInUser()
        {
            Email = "@gmail.com",
            Password = "TTCGCghcvhj"
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
        var user = new ChangePasswordDto
        {
            Email = "Name1@gmail.com",
            CurrentPassword = "12345678",
            NewPassword = "123456789",
            ConfirmPassword = "123456789"
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
        var user = new ChangePasswordDto
        {
            Email = "@gmail.com",
            CurrentPassword = "12345678",
            NewPassword = "123456789",
            ConfirmPassword = "123456789"
        };

        //act
        var result = await controller.ChangePassword(user);
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
        var user = new ChangeEmailDto
        {
            CurrentEmail = "vadim@gmail.com",
            NewEmail = "vova@gmail.com"
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
        var user = new ChangeEmailDto
        {
            CurrentEmail = "@gmail.com",
            NewEmail = "vova@gmail.com"
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
    [Fact]
    public async Task ConfirmEmail_ShouldReturnOkStatus_WhenEmailWasConfirmed()
    {
        //arrange
        _account.ConfirmEmailAsync(Arg.Any<string>(),Arg.Any<string>()).Returns(IdentityResult.Success);
        var controller = new AccountController(_account);
        var email = "vavsye38sd@happy2023year.com";
        var token = "Q2ZESjhIKzduanVwQjF4T3IvM0VzT3FwaWNYaDErZEpubkdZY3BCRTN5WlBWWTVkSTZJZ1Ayelo0QzViOHhlTlNCZFVVbjE0Q0k0WDN6ZEg1U3kzekhEbTd4SFcvdHhxRXErZEdzY1JtTXJVTW5jaG4zZ1JsUGM2a3RNaEt0dXRxa2kvbEs1TFJ1ekJTN3hadzI5dDBGOTR0aG9HZlBuSjFpZE4rTTVzeHdGaXlkN0xLMkJ5TGp6Nno3d3g0L1A2TmMrYnRmeWlQSnFNYzNoL29XcVh0ZndmSnF3OFlFbldSa3lWeUZjMmIvSmF4eDlBSy81UURzU1VHcno3L3pGUit4eVZrdz09";

        //act
        var result = await controller.ConfirmEmail(email, token) as OkObjectResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);

    }
    [Fact]
    public async Task ConfirmEmail_ShouldReturnNotFound_WhenEmailIsInvalid()
    {
        //arrange
        _account.ConfirmEmailAsync(Arg.Any<string>(),Arg.Any<string>()).Returns(IdentityResult.Failed());
        var controller = new AccountController(_account);
        var email = "---@happy2023year.com";
        var token = "Q2ZESjhIKzduanVwQjF4T3IvM0VzT3FwaWNYaDErZEpubkdZY3BCRTN5WlBWWTVkSTZJZ1Ayelo0QzViOHhlTlNCZFVVbjE0Q0k0WDN6ZEg1U3kzekhEbTd4SFcvdHhxRXErZEdzY1JtTXJVTW5jaG4zZ1JsUGM2a3RNaEt0dXRxa2kvbEs1TFJ1ekJTN3hadzI5dDBGOTR0aG9HZlBuSjFpZE4rTTVzeHdGaXlkN0xLMkJ5TGp6Nno3d3g0L1A2TmMrYnRmeWlQSnFNYzNoL29XcVh0ZndmSnF3OFlFbldSa3lWeUZjMmIvSmF4eDlBSy81UURzU1VHcno3L3pGUit4eVZrdz09";

        //act
        var result = await controller.ConfirmEmail(email, token) as NotFoundResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);

    }
    [Fact]
    public async Task ConfirmEmail_ShouldReturnNotFound_WhenTokenIsInvalid()
    {
        //arrange
        _account.ConfirmEmailAsync(Arg.Any<string>(),Arg.Any<string>()).Returns(IdentityResult.Failed());
        var controller = new AccountController(_account);
        var email = "vavsye38sd@happy2023year.com";
        var token = "Q2dz09";

        //act
        var result = await controller.ConfirmEmail(email, token) as NotFoundResult;
        var statusCode = result.StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.NotFound);

    }
}
