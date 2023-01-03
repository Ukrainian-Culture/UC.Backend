using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;

namespace Entities.DTOs;

public class ArticleTileDto
{
    public int ArticleId { get; init; }
    public string Type { get; init; } = null!;
    public string Region { get; init; } = null!;
    public string SubText { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Category { get; set; } = null!;
}