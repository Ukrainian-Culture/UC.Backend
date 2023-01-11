namespace Entities.DTOs;

public class ArticleToUpdateDto
{
    public string Type { get; init; } = null!;
    public string Region { get; init; } = null!;
    public string Date { get; set; } = null!;
    public Guid CategoryId { get; init; }
}