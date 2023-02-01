using Contracts;

namespace Ukranian_Culture.Backend.Services;

public class ErrorMessageProvider : IErrorMessageProvider
{
    public string NotFoundMessage<T>(Guid id)
        => $"{typeof(T).Name} with id: \"{id}\" doesn't exist in database";

    public string BadRequestMessage<T>()
        => $"{typeof(T).Name} object is null";
}