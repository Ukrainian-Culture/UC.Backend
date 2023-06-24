using Ukranian_Culture.Backend.Services;

namespace Ukrainian_Culture.Tests.ServicesTests;

public class ErrorMessageProviderTests
{
    [Fact]
    public void NotFoundMessage_ShouldReturnCorrectMessage()
    {
        Guid testableId = new("a757f007-0222-470c-8aca-b6220a5da944");
        const string testableMessage = "hello";
        NotFoundMessage_Helper<int, Guid>(
            "Int32 with property Guid and value: a757f007-0222-470c-8aca-b6220a5da944 doesn't exist in database",
            testableId);
        NotFoundMessage_Helper<Article, Guid>(
            "Article with property Guid and value: a757f007-0222-470c-8aca-b6220a5da944 doesn't exist in database",
            testableId);
        NotFoundMessage_Helper<string, Guid>(
            "String with property Guid and value: a757f007-0222-470c-8aca-b6220a5da944 doesn't exist in database",
            testableId);
        NotFoundMessage_Helper<string, string>(
            "String with property String and value: hello doesn't exist in database",
            testableMessage);
    }

    private void NotFoundMessage_Helper<TErrType, TErrValue>(string message, TErrValue value)
    {
        //Arrange
        ErrorMessageProvider messageProvider = new();
        //Act
        var receiveMessage = messageProvider.NotFoundMessage<TErrType, TErrValue>(value);
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