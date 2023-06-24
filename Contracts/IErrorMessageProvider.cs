namespace Contracts;

public interface IErrorMessageProvider
{
    string NotFoundMessage<TErrType, TErrValue>(TErrValue value);
    string BadRequestMessage<T>();
}