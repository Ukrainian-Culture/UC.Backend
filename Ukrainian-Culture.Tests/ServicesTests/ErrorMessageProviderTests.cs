using Ukranian_Culture.Backend.Services;

namespace Ukrainian_Culture.Tests.ServicesTests;

public class ErrorMessageProviderTests
{
    [Fact]
    public void NotFoundMessage_ShouldReturnCorrectMessage()
    {
        Guid testableId = new("a757f007-0222-470c-8aca-b6220a5da944");
        NotFoundMessage_Helper<int>($"Int32 with id: \"a757f007-0222-470c-8aca-b6220a5da944\" doesn't exist in database",testableId);
        NotFoundMessage_Helper<Article>($"Article with id: \"a757f007-0222-470c-8aca-b6220a5da944\" doesn't exist in database", testableId);
        NotFoundMessage_Helper<string>($"String with id: \"a757f007-0222-470c-8aca-b6220a5da944\" doesn't exist in database", testableId);
    }

    private void NotFoundMessage_Helper<T>(string message, Guid idOfTestedObj)
    {
        //Arrange
        ErrorMessageProvider messageProvider = new();
        //Act
        var receiveMessage = messageProvider.NotFoundMessage<T>(idOfTestedObj);
        //Assert
        receiveMessage.Should().Be(message);
    }

    [Fact]
    public void BadRequestMessage_ShouldReturnCorrectMessage()
    {
        Guid testableId = new("a757f007-0222-470c-8aca-b6220a5da944");
        BadRequestMessage_Helper<int>("Int32 object is null");
        BadRequestMessage_Helper<Article>("Article object is null");
        BadRequestMessage_Helper<string>("String object is null");
    }

    private void BadRequestMessage_Helper<T>(string message)
    {
        //Arrange
        ErrorMessageProvider messageProvider = new();
        //Act
        var receiveMessage = messageProvider.BadRequestMessage<T>();
        //Assert
        receiveMessage.Should().Be(message);
    }
}