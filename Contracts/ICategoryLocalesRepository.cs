using Entities.Models;

namespace Contracts;

public interface ICategoryLocalesRepository
{
    Task<List<CategoryLocale>> GetCategoriesByCulture(int cultureId, ChangesType trackChanges);
}