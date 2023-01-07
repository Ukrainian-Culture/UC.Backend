using Entities.Models;

namespace Contracts;

public interface ICultureRepository
{
    Task<Culture> GetCultureWithContentAsync(int cultureId, ChangesType asNoTracking);
}