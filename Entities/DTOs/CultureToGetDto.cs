namespace Entities.DTOs;

public class CultureToGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string DisplayedName { get; set; } = null!;
}