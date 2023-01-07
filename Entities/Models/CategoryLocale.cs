using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class CategoryLocale
{
    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(Culture))] public Guid CultureId { get; set; }
    public Culture Culture { get; set; }
    public string Name { get; set; } = null!;
}