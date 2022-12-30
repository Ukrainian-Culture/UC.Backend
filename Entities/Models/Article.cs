using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Article
{
    public int Id { get; init; }
    public string Type { get; init; } = null!;
    public string Region { get; init; } = null!;

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; init; }
    public Category Category { get; set; } = null!;

    [ForeignKey(nameof(Info))]
    public int InfoId { get; init; }
    public Info Info { get; set; } = null!;
}