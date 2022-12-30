using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class CategoryLocale
{
    public int CategoryId { get; set; }
    [ForeignKey(nameof(Culture))] public int CultureId { get; set; }
    public Culture Culture { get; set; }
    public string Name { get; set; } = null!;
}