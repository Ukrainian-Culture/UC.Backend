namespace Entities.DTOs;

public class ArticlesLocaleToGetDto
{
    public Guid ArticleId { get; set; }
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public string SubText { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string? Region { get; set; }
    public string? Category { get; set; }
}