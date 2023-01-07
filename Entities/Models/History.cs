using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class History
{
    public string Title { get; init; } = null!;

    [ForeignKey(nameof(Article))]
    public Guid ArticleId { get; init; }
    public Article Article { get; init; } = null!;
}