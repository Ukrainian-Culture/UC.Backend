namespace Entities.DTOs;

public class UserHistoryToGetDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string DateOfWatch { get; set; } = null!;
    public string Title { get; set; } = null!;
}