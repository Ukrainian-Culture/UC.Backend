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
    public DbSet<ArticlesLocale> ArticlesLocales { get; set; } = null!;
    public DbSet<Culture> Cultures { get; set; } = null!;
    public DbSet<UserHistory> UsersHistories { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedData.Seed(modelBuilder);

        modelBuilder.Entity<ArticlesLocale>().HasKey(article => new { article.Id, article.CultureId });

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.ArticlesTranslates)
            .WithOne(a => a.Culture);

        modelBuilder.Entity<User>()
            .HasMany(a => a.History)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

        modelBuilder.ApplyConfiguration(new ArticlesConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfigurations());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleLocaleConfiguration());
        modelBuilder.ApplyConfiguration(new CultureConfiguration());
        modelBuilder.ApplyConfiguration(new UsersHistoryConfigurations());

    }
}