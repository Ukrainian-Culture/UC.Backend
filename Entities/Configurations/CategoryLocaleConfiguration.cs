using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CategoryLocaleConfiguration : IEntityTypeConfiguration<CategoryLocale>
{
    private readonly CultureConfiguration _cultureConfig = new();
    private readonly CategoryConfiguration _categoryConfig = new();
    public void Configure(EntityTypeBuilder<CategoryLocale> builder)
    {
        builder.HasData(
            new CategoryLocale
            {
                CategoryId = _categoryConfig.FirstId,
                CultureId = _cultureConfig.FirstId,
                Name = "People"
            },
            new CategoryLocale
            {
                CategoryId = _categoryConfig.FirstId,
                CultureId = _cultureConfig.SecondId,
                Name = "Люди"
            },
            new CategoryLocale
            {
                CategoryId = _categoryConfig.SecondId,
                CultureId = _cultureConfig.FirstId,
                Name = "Food"
            },
            new CategoryLocale
            {
                CategoryId = _categoryConfig.SecondId,
                CultureId = _cultureConfig.SecondId,
                Name = "Їжа"
            }
        );
    }
}