using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class ArticlesConfiguration : IEntityTypeConfiguration<Article>
{

    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasData(
            new Article
            {
                Id = 1,
                Type = "file",
                Region = "hmelnytsk",
                CategoryId = 1,
                InfoId = 1
            },
            new Article
            {
                Id = 2,
                Type = "file",
                Region = "Kyiv",
                CategoryId = 1,
                InfoId = 2
            }
        );
    }
}