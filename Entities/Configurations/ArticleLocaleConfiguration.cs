using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class ArticleLocaleConfiguration : IEntityTypeConfiguration<ArticlesLocale>
{
    CultureConfiguration CultureConfig=new CultureConfiguration();
    public Guid firstID = new Guid("e847e218-1be2-40c2-9d44-d4c93bbf493b");
    public Guid secondID = new Guid("0a2e4bf1-ce88-4008-8e7b-ad6855572a6d");
    public void Configure(EntityTypeBuilder<ArticlesLocale> builder)
    {
        builder.HasData(
            new ArticlesLocale
            {
                Id = firstID,
                CultureId = CultureConfig.firstID,
                Title = "About Bohdan Khmelnytsky",
                Content = "About Bohdan Khmelnytsky .... ",
                SubText = "About Bohdan Khmelnytsky",
                ShortDescription = "About Bohdan Khmelnytsky"
            },
            new ArticlesLocale
            {
                Id= firstID,
                CultureId = CultureConfig.secondID,
                Title = "Про Богдана Хмельницького",
                Content = "Про Богдана Хмельницького .... ",
                SubText = "Про Богдана Хмельницького",
                ShortDescription = "Про Богдана Хмельницького"
            },
            new ArticlesLocale
            {
                Id = secondID,
                CultureId = CultureConfig.firstID,
                Title = "About Ivan Mazepa",
                Content = "About Ivan Mazepa .... ",
                ShortDescription = "About Ivan Mazepa",
                SubText = "About Ivan Mazepa"
            },
            new ArticlesLocale
            {
                Id = secondID,
                CultureId = CultureConfig.secondID,
                Title = "Про Івана Мазепу",
                Content = "Про Івана Мазепу .... ",
                ShortDescription = "Про Івана Мазепу",
                SubText = "Про Івана Мазепу"
            }
        );
    }
}