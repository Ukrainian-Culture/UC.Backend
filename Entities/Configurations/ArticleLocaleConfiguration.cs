using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class ArticleLocaleConfiguration : IEntityTypeConfiguration<ArticlesLocale>
{
    private readonly CultureConfiguration _cultureConfig = new();
    private readonly ArticlesConfiguration _articlesConfiguration = new();
    public void Configure(EntityTypeBuilder<ArticlesLocale> builder)
    {
        builder.HasData(
            new ArticlesLocale
            {
                Id = _articlesConfiguration.FirstId,
                CultureId = _cultureConfig.FirstId,
                Title = "About Bohdan Khmelnytsky",
                Content = "About Bohdan Khmelnytsky .... ",
                SubText = "About Bohdan Khmelnytsky",
                ShortDescription = "About Bohdan Khmelnytsky"
            },
            new ArticlesLocale
            {
                Id = _articlesConfiguration.FirstId,
                CultureId = _cultureConfig.SecondId,
                Title = "Про Богдана Хмельницького",
                Content = "Про Богдана Хмельницького .... ",
                SubText = "Про Богдана Хмельницького",
                ShortDescription = "Про Богдана Хмельницького"
            },
            new ArticlesLocale
            {
                Id = _articlesConfiguration.SecondId,
                CultureId = _cultureConfig.FirstId,
                Title = "About Ivan Mazepa",
                Content = "About Ivan Mazepa .... ",
                ShortDescription = "About Ivan Mazepa",
                SubText = "About Ivan Mazepa"
            },
            new ArticlesLocale
            {
                Id = _articlesConfiguration.SecondId,
                CultureId = _cultureConfig.SecondId,
                Title = "Про Івана Мазепу",
                Content = "Про Івана Мазепу .... ",
                ShortDescription = "Про Івана Мазепу",
                SubText = "Про Івана Мазепу"
            }
        );
    }
}