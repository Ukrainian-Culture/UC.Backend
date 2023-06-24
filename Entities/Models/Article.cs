using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Article
{
    public Guid Id { get; init; }
    public string Region { get; init; } = null!;
    public DateTime Date { get; set; }

    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; init; }
    public Category Category { get; set; } = null!;

}