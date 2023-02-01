using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public Guid FirstId = new("858feff1-770f-4090-922a-a8dd9b16e0ee");
    public Guid SecondId = new("0e5809cd-d66e-4b1d-ac25-27a36750ebbd");
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = FirstId,
                Name = "Music"
            },
            new Category
            {
                Id = SecondId,
                Name = "People"
            });
    }
}