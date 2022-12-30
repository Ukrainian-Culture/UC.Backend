namespace Entities.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Phone { get; set; } = null!;
}