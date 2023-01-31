namespace Entities.DTOs;

public class ArticleToGetDto
{
    public Guid Id { get; init; }
    public Guid CategoryId { get; init; }
    public string Region { get; init; } = null!;
    public string Date { get; set; } = null!;
}