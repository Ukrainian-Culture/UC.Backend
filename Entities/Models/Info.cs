namespace Entities.Models;

public class Info
{
    public int Id { get; init; }
    public string SubText { get; init; } = null!;
    public string ShortDesc { get; init; } = null!;
    public DateTime Date { get; init; }
}