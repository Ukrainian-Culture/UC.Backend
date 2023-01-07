using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CultureConfiguration : IEntityTypeConfiguration<Culture>
{
    public Guid firstID = new Guid("4fd5d8c1-f34b-4824-b252-69910285e681");
    public Guid secondID = new Guid("0a315a0f-4860-4302-bb79-dec86e87d378");
    public void Configure(EntityTypeBuilder<Culture> builder)
    {
        builder.HasData(
            new Culture
            {
                Id = firstID,
                Name = "en",
                DisplayedName = "English"
            }, new Culture
            {
                Id = secondID,
                Name = "ua",
                DisplayedName = "Ukrainian"
            }
        );
    }
}