namespace Entities.Models;

public class UserHistory
{
    public Guid Id { get; set; }
    public DateTime DateOfWatch { get; set; }
    public string Title { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}