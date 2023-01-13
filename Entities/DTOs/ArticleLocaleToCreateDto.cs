namespace Entities.DTOs;

public class ArticleLocaleToCreateDto
{
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public string SubText { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
}