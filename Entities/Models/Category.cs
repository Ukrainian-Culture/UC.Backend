namespace Entities.Models;

public class Category
{
    public int Id { get; init; }
    public ICollection<Article> Articles { get; init; } = null!;
}