using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Article
{
    public int Id { get; init; }
    public string Type { get; init; } = null!;
    public string Region { get; init; } = null!;
    public DateTime Date { get; set; }

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; init; }
    public Category Category { get; set; } = null!;

}