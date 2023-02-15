using System.ComponentModel.DataAnnotations.Schema;
using Entities.DTOs;

namespace Entities.Models;

public class UserHistory
{
    public Guid Id { get; set; }
    public DateTime DateOfWatch { get; set; }
    
    public Guid ArticleId { get; init; }
    public string Region { get; init; } = null!;
    public string SubText { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Category { get; set; } = null!;

    [ForeignKey(nameof(User))] public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}