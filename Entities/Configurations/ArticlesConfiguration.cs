using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class ArticlesConfiguration : IEntityTypeConfiguration<Article>
{
    CategoryConfiguration CategoryConfig=new CategoryConfiguration();
    public Guid firstID = new Guid("5eca5808-4f44-4c4c-b481-72d2bdf24203");
    public Guid secondID = new Guid("5b32effd-2636-4cab-8ac9-3258c746aa53");
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasData(
            new Article
            {
                Id = firstID,
                Type = "file",
                Region = "hmelnytsk",
                CategoryId = CategoryConfig.firstID,
                Date = new DateTime(1886, 2, 1)
            },
            new Article
            {
                Id = secondID,
                Type = "file",
                Region = "Kyiv",
                CategoryId = CategoryConfig.firstID,
                Date = new DateTime(2001, 01, 01)
            }
        );
    }
}