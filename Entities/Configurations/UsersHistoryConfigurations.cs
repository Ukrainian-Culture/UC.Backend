using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class UsersHistoryConfigurations : IEntityTypeConfiguration<UserHistory>
{
    public void Configure(EntityTypeBuilder<UserHistory> builder)
    {
        builder.HasData(
            new List<UserHistory>
            {
                new()
                {
                    Id = new Guid("C5A0E131-46A0-4F37-9A9D-6E426CB94F46"),
                    DateOfWatch = new DateTime(2023, 1, 18, 1, 1, 1, 1, DateTimeKind.Utc),
                    Title = "About Bohdan Khmelnytsky",
                    Region = "Kyiv",
                    Category = "Music",
                    SubText = "Text",
                    ArticleId = new("5eca5808-4f44-4c4c-b481-72d2bdf24203"),
                    UserId = new Guid("169a9df2-231c-45e8-9a0a-c7333f0dc9f4")
                },
                new()
                {
                    Id = new Guid("9D2ABE54-D8FB-45EB-94A0-65CEFCBFA432"),
                    DateOfWatch = new DateTime(2023, 1, 18, 1, 3, 1, DateTimeKind.Utc),
                    Title = "About Ivan Mazepa",
                    Region = "Kyiv",
                    SubText = "text2",
                    Category = "People",
                    ArticleId = new("5b32effd-2636-4cab-8ac9-3258c746aa53"),
                    UserId = new Guid("87d76511-8b74-4250-aef1-c47b8cb9308f")
                }
            });
    }
}