using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class InfoConfiguration : IEntityTypeConfiguration<Info>
{
    public void Configure(EntityTypeBuilder<Info> builder)
    {
        builder.HasData(
            new Info
            {
                Id = 1,
                SubText = "About Bohdan Khmelnytsky",
                ShortDesc = "was a Ruthenian military commander and Hetman of the Zaporozhian Host",
                Date = new DateTime(1667, 2, 5)
            },
            new Info
            {
                Id = 2,
                SubText = "About Ivan Mazepa",
                ShortDesc = "a was a Ukrainian military, political, and civic leader who served as the Hetman of Zaporizhian Host in 1687–1708",
                Date = new DateTime(1687, 4, 1)
            });
    }
}