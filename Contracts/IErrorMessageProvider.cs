namespace Contracts;

public interface IErrorMessageProvider
{
    string NotFoundMessage<T>(Guid id);
    string BadRequestMessage<T>();
}