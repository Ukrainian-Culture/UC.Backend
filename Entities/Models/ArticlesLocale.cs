using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class ArticlesLocale
{
    public Guid Id { get; init; }
    [ForeignKey(nameof(Culture))] public Guid CultureId { get; init; }
    public Culture Culture { get; set; } = null!;

    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public string SubText { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
}