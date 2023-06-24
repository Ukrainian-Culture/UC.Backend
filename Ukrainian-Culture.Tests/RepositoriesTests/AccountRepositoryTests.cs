
namespace Ukrainian_Culture.Tests.RepositoriesTests;

public class AccountRepositoryTests
{
    private readonly IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();

    [Fact]
    public async Task SignUpAsync_ShouldReturnSuccess_WhenUserAreValid()
    {
        //Arrange
        var expected = IdentityResult.Success;
        _accountRepository.SignUpAsync(Arg.Any<SignUpUser>()).Returns(expected);

        var user = new SignUpUser
        {
            Email = "abc@gmail.com",
            Password = "345rt6ty6",
            ConfirmPassword = "345rt6ty6"
        };

        //Act
        var result = await _accountRepository.SignUpAsync(user);

        //Asert
        result.Equals(expected);
    }

    [Fact]
    public async Task SignUpAsync_ShouldReturnFailed_WhenUserEmailAreInvalid()
    {
        //Arrange
        var expected = IdentityResult.Failed();
        _accountRepository.SignUpAsync(Arg.Any<SignUpUser>()).Returns(expected);

        var user = new SignUpUser
        {
            Email = "abc.com",
            Password = "345rt6ty6",
            ConfirmPassword = "345rt6ty6"
        };

        //Act
        var result = await _accountRepository.SignUpAsync(user);

        //Asert
        result.Equals(expected);
    }

    [Fact]
    public async Task SignUpAsync_ShouldReturnFailed_WhenPasswordNotConfirmed()
    {
        //Arrange
        var expected = IdentityResult.Failed();
        _accountRepository.SignUpAsync(Arg.Any<SignUpUser>()).Returns(expected);
        var user = new SignUpUser
        {
            Email = "abc@gmail.com",
            Password = "3",
            ConfirmPassword = "12345678"
        };

        //Act
        var result = await _accountRepository.SignUpAsync(user);

        //Asert
        result.Equals(expected);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnTokenModel_WhenUserAreValid()
    {
        //Arrange
        var expected = new TokenModel();
        _accountRepository.LoginAsync(Arg.Any<SignInUser>()).Returns(expected);

        var user = new SignInUser()
        {
            Email = "Volodya22@gmail.com",
            Password = "TTCGCghcvhj"
        };

        //Act
        var result = await _accountRepository.LoginAsync(user);

        //Asert
        Assert.Equal(result, expected);
    }


    [Fact]
    public async Task LoginAsync_ShouldReturnNull_WhenEmailAreInvalid()
    {
        //Arrange
        _accountRepository.LoginAsync(Arg.Any<SignInUser>()).ReturnsNull();

        var user = new SignInUser()
        {
            Email = "2gmail.com",
            Password = "TTCGCghcvhj"
        };

        //Act
        var result = await _accountRepository.LoginAsync(user);

        //Asert
        result.Should().BeNull();
    }

    [Fact]
    public async Task ChangeEmailAsync_ShouldReturnSuccess_WhenChangeEmailDtoValid()
    {
        //Arrange
        var expected = IdentityResult.Success;
        _accountRepository.ChangeEmailAsync(Arg.Any<ChangeEmailDto>()).Returns(expected);

        var email = new ChangeEmailDto
        {
            CurrentEmail = "abc@gmail.com",
            NewEmail = "asd@gmail.com"
        };

        //Act
        var result = await _accountRepository.ChangeEmailAsync(email);

        //Asert
        result.Equals(expected);
    }

    [Fact]
    public async Task ChangeEmailAsync_ShouldReturnFailed_WhenChangeEmailDtoInValid()
    {
        //Arrange
        var expected = IdentityResult.Failed();
        _accountRepository.ChangeEmailAsync(Arg.Any<ChangeEmailDto>()).Returns(expected);

        var email = new ChangeEmailDto
        {
            CurrentEmail = "abcgmail.com",
            NewEmail = "asd@gmail.com"
        };

        //Act
        var result = await _accountRepository.ChangeEmailAsync(email);

        //Asert
        result.Equals(expected);
    }

    [Fact]
    public async Task ChangePasswordAsync_ShouldReturnSuccess_WhenChangePasswordDtoValid()
    {
        //Arrange
        var expected = IdentityResult.Success;
        _accountRepository.ChangePasswordAsync(Arg.Any<ChangePasswordDto>()).Returns(expected);

        var userChangeData = new ChangePasswordDto
        {
            CurrentPassword = "12345678",
            NewPassword = "87654321",
            ConfirmPassword = "87654321",
            Email = "abc@gmail.com"
        };

        //Act
        var result = await _accountRepository.ChangePasswordAsync(userChangeData);

        //Asert
        result.Equals(expected);
    }

    [Fact]
    public async Task ChangePasswordAsync_ShouldReturnFailed_WhenChangePasswordDtoInValid()
    {
        //Arrange
        var expected = IdentityResult.Failed();
        _accountRepository.ChangePasswordAsync(Arg.Any<ChangePasswordDto>()).Returns(expected);

        var userChangeData = new ChangePasswordDto
        {
            CurrentPassword = "12345678",
            NewPassword = "87654321",
            ConfirmPassword = "123",
            Email = "abc@gmail.com"
        };

        //Act
        var result = await _accountRepository.ChangePasswordAsync(userChangeData);

        //Asert
        result.Equals(expected);
    }

    [Fact]
    public async Task DeleteAccountAsync_ShouldReturnSuccess_WhenUserRegister()
    {
        //Arrange
        var expected = IdentityResult.Success;
        _accountRepository.DeleteAccountAsync(Arg.Any<Guid>()).Returns(expected);
        var Id = new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203");

        //Act
        var result = await _accountRepository.DeleteAccountAsync(Id);

        //Asert
        result.Equals(expected);
    }

    [Fact]
    public async Task DeleteAccountAsync_ShouldReturnFailed_WhenUserNotRegister()
    {
        //Arrange
        var expected = IdentityResult.Failed();
        _accountRepository.DeleteAccountAsync(Arg.Any<Guid>()).Returns(expected);
        var Id = new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203");

        //Act
        var result = await _accountRepository.DeleteAccountAsync(Id);

        //Asert
        result.Equals(expected);
    }
}
