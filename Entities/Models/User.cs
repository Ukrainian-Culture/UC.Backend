namespace Entities.Models;

public class User
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Login { get; init; } = null!;
    public string Phone { get; init; } = null!;
}