using Entities.Models;

namespace Entities.DTOs;

public class HistoryToCreateDto
{
    public Guid ArticleId { get; init; }
    public string Region { get; init; } = null!;
    public string SubText { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Category { get; set; } = null!;
}