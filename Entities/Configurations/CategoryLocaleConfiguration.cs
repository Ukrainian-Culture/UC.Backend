using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CategoryLocaleConfiguration : IEntityTypeConfiguration<CategoryLocale>
{
    CultureConfiguration CultureConfig=new CultureConfiguration();
    CategoryConfiguration CategoryConfig=new CategoryConfiguration();
    public void Configure(EntityTypeBuilder<CategoryLocale> builder)
    {
        builder.HasData(
            new CategoryLocale
            {
                CategoryId = CategoryConfig.firstID,
                CultureId = CultureConfig.firstID,
                Name = "People"
            },
            new CategoryLocale
            {
                CategoryId = CategoryConfig.firstID,
                CultureId = CultureConfig.secondID,
                Name = "Люди"
            },
            new CategoryLocale
            {
                CategoryId = CategoryConfig.secondID,
                CultureId = CultureConfig.firstID,
                Name = "Food"
            },
            new CategoryLocale
            {
                CategoryId = CategoryConfig.secondID,
                CultureId = CultureConfig.secondID,
                Name = "Їжа"
            }
        );
    }
}