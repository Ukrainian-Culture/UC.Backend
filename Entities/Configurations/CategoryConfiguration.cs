using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public Guid firstID = new Guid("858feff1-770f-4090-922a-a8dd9b16e0ee");
    public Guid secondID = new Guid("0e5809cd-d66e-4b1d-ac25-27a36750ebbd");
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = firstID,
            },
            new Category
            {
                Id = secondID,
            });
    }
}