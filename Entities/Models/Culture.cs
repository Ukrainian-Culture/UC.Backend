using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Culture
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string DisplayedName { get; set; } = null!;
    public ICollection<ArticlesLocale> ArticlesTranslates { get; set; }
}