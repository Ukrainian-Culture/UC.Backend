using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryLocale>().HasKey(ct => new { ct.CategoryId, ct.CultureId });
        modelBuilder.Entity<ArticlesLocale>().HasKey(article => new { article.Id, article.CultureId });

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.ArticlesTranslates)
            .WithOne(a => a.Culture);

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.Categories)
            .WithOne(a => a.Culture);
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<Info> Info { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<CategoryLocale> CategoryLocales { get; set; } = null!;
    public DbSet<ArticlesLocale> ArticlesLoxaLocales { get; set; } = null!;
    public DbSet<Culture> Cultures { get; set; } = null!;
}