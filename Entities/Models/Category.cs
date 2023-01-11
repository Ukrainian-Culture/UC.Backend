using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Category
{
    public Guid Id { get; init; }
    public ICollection<Article> Articles { get; init; } = null!;
}