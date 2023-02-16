namespace Entities.DTOs;

public class UserHistoryToGetDto
{
    public Guid ArticleId { get; init; }
    public string DateOfWatch { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Region { get; init; } = null!;
    public string SubText { get; set; } = null!;
    public string? Category { get; set; }
}