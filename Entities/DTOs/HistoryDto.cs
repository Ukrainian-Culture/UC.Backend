namespace Entities.DTOs;

public class HistoryDto
{
    public int ActicleId { get; set; }
    public string Region { get; set; } = null!;
    public DateTime Date { get; set; }
    public string SubText { get; set; } = null!;
}

