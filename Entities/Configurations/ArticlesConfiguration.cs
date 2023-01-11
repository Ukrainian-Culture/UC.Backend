using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class ArticlesConfiguration : IEntityTypeConfiguration<Article>
{
    private readonly CategoryConfiguration _categoryConfig = new();
    public Guid FirstId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203");
    public Guid SecondId = new("5b32effd-2636-4cab-8ac9-3258c746aa53");
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasData(
            new Article
            {
                Id = FirstId,
                Type = "file",
                Region = "hmelnytsk",
                CategoryId = _categoryConfig.FirstId,
                Date = new DateTime(1886, 2, 1)
            },
            new Article
            {
                Id = SecondId,
                Type = "file",
                Region = "Kyiv",
                CategoryId = _categoryConfig.FirstId,
                Date = new DateTime(2001, 01, 01)
            }
        );
    }
}