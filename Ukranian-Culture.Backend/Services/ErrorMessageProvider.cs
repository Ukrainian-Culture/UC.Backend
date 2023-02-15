using Contracts;

namespace Ukranian_Culture.Backend.Services;

public class ErrorMessageProvider : IErrorMessageProvider
{
    public string NotFoundMessage<TErrType, TErrValue>(TErrValue value)
        => $"{typeof(TErrType).Name} with property {typeof(TErrValue).Name} and value: {value} doesn't exist in database";

    public string BadRequestMessage<T>()
        => $"{typeof(T).Name} object is null";
}