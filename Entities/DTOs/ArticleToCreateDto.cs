namespace Entities.DTOs;

public class ArticleToCreateDto
{
    public string Type { get; init; } = null!;
    public string Region { get; init; } = null!;
    public string Date { get; set; } = null!;
    public int CategoryId { get; init; }
}