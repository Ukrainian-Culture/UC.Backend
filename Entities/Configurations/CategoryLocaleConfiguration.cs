using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CategoryLocaleConfiguration : IEntityTypeConfiguration<CategoryLocale>
{
    public void Configure(EntityTypeBuilder<CategoryLocale> builder)
    {
        builder.HasData(
            new CategoryLocale
            {
                CategoryId = 1,
                CultureId = 1,
                Name = "People"
            },
            new CategoryLocale
            {
                CategoryId = 1,
                CultureId = 2,
                Name = "Люди"
            },
            new CategoryLocale
            {
                CategoryId = 2,
                CultureId = 1,
                Name = "Food"
            },
            new CategoryLocale
            {
                CategoryId = 2,
                CultureId = 2,
                Name = "Їжа"
            }
        );
    }
}