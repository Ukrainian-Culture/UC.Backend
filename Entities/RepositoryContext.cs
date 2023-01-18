using Entities.Configurations;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class RepositoryContext : IdentityDbContext<User, Roles, Guid>
{
    public RepositoryContext()
    {
    }
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<CategoryLocale> CategoryLocales { get; set; } = null!;
    public DbSet<ArticlesLocale> ArticlesLocales { get; set; } = null!;
    public DbSet<Culture> Cultures { get; set; } = null!;
    public DbSet<UserHistory> UsersHistories { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedRoles(modelBuilder);

        modelBuilder.Entity<CategoryLocale>().HasKey(ct => new { ct.CategoryId, ct.CultureId });
        modelBuilder.Entity<ArticlesLocale>().HasKey(article => new { article.Id, article.CultureId });

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.ArticlesTranslates)
            .WithOne(a => a.Culture);

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.Categories)
            .WithOne(a => a.Culture);
        
        modelBuilder.Entity<User>()
            .HasOne(a => a.History)
            .WithOne(a => a.User);
        
        modelBuilder.ApplyConfiguration(new ArticlesConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfigurations());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleLocaleConfiguration());
        modelBuilder.ApplyConfiguration(new CultureConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryLocaleConfiguration());
        modelBuilder.ApplyConfiguration(new UsersHistoryConfigurations());

    }

    private void SeedRoles(ModelBuilder builder)
    {
        var firstId = new Guid("431f29e9-13ff-4f5f-b178-511610d5103f");
        var SecondId = new Guid("5adbec33-97c5-4041-be6a-e0f3d3ca6f44");
        builder.Entity<Roles>().HasData(
            new Roles() { Id = firstId, Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new Roles() { Id = SecondId, Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
            );
    }

}