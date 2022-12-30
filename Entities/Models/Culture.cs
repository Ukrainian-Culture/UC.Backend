namespace Entities.Models;

public class Culture
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string DisplayedName { get; set; } = null!;

    public ICollection<CategoryLocale> Categories { get; set; }
    public ICollection<ArticlesLocale> ArticlesTranslates { get; set; }
}