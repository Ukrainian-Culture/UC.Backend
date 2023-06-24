using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;

namespace Entities.DTOs;

public class HistoryDto
{
    public Guid ActicleId { get; set; }
    public string Region { get; set; } = null!;
    public string Date { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
}