using Contracts;

namespace Ukranian_Culture.Backend.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentTime() => DateTime.Now;
}