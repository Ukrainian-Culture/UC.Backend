using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ukrainian_Culture.Tests.DbModels;

public class ArticleModel : ITestableModel
{
    public IModel GetModel()
    {
        var modelBuilder = new ModelBuilder();
        modelBuilder.Entity<Article>().HasKey(article => article.Id);

        modelBuilder.Entity<Article>()
            .HasOne(article => article.Category)
            .WithMany(cat => cat.Articles);

        modelBuilder.Entity<Article>().Property(article => article.Date).IsRequired();
        modelBuilder.Entity<Article>().Property(article => article.Region).IsRequired();
        modelBuilder.Entity<Article>().Property(article => article.Type).IsRequired();
        modelBuilder.Entity<Article>().Property(article => article.CategoryId).IsRequired();

        modelBuilder.Entity<Category>().HasKey(category => category.Id);
        modelBuilder.Entity<Category>()
            .HasMany(cat => cat.Articles)
            .WithOne(art => art.Category);

        return modelBuilder.FinalizeModel();
    }
}