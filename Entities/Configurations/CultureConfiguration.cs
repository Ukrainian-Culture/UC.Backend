using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CultureConfiguration : IEntityTypeConfiguration<Culture>
{
    public void Configure(EntityTypeBuilder<Culture> builder)
    {
        builder.HasData(
            new Culture
            {
                Id = 1,
                Name = "en",
                DisplayedName = "English"
            }, new Culture
            {
                Id = 2,
                Name = "ua",
                DisplayedName = "Ukrainian"
            }
        );
    }
}