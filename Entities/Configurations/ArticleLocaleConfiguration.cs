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
                Content = "About Bohdan Khmelnytsky .... "
            },
            new ArticlesLocale
            {
                Id = 1,
                CultureId = 2,
                Title = "Про Богдана Хмельницького",
                Content = "Про Богдана Хмельницького .... "
            },
            new ArticlesLocale
            {
                Id = 2,
                CultureId = 1,
                Title = "About Ivan Mazepa",
                Content = "About Ivan Mazepa .... "
            },
            new ArticlesLocale
            {
                Id = 2,
                CultureId = 2,
                Title = "Про Івана Мазепу",
                Content = "Про Івана Мазепу .... "
            }
        );
    }
}