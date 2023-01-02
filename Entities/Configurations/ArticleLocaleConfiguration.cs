using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class ArticleLocaleConfiguration : IEntityTypeConfiguration<ArticlesLocale>
{
    public void Configure(EntityTypeBuilder<ArticlesLocale> builder)
    {
        builder.HasData(
            new ArticlesLocale
            {
                Id = 1,
                CultureId = 1,
                Title = "About Bohdan Khmelnytsky",
                Content = "About Bohdan Khmelnytsky .... ",
                SubText = "About Bohdan Khmelnytsky",
                ShortDescription = "About Bohdan Khmelnytsky"
            },
            new ArticlesLocale
            {
                Id = 1,
                CultureId = 2,
                Title = "Про Богдана Хмельницького",
                Content = "Про Богдана Хмельницького .... ",
                SubText = "Про Богдана Хмельницького",
                ShortDescription = "Про Богдана Хмельницького"
            },
            new ArticlesLocale
            {
                Id = 2,
                CultureId = 1,
                Title = "About Ivan Mazepa",
                Content = "About Ivan Mazepa .... ",
                ShortDescription = "About Ivan Mazepa",
                SubText = "About Ivan Mazepa"
            },
            new ArticlesLocale
            {
                Id = 2,
                CultureId = 2,
                Title = "Про Івана Мазепу",
                Content = "Про Івана Мазепу .... ",
                ShortDescription = "Про Івана Мазепу",
                SubText = "Про Івана Мазепу"
            }
        );
    }
}