using Entities.Configurations;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ukrainian_Culture.Tests.DbModels;

public class CultureModel : ITestableModel
{
    public IModel GetModel()
    {

        var modelBuilder = new ModelBuilder();
        ApplyCultureTable(modelBuilder);
        ApplyCategoriesLocaleTable(modelBuilder);
        ApplyArticlesLocaleTable(modelBuilder);
        return modelBuilder.FinalizeModel();
    }

    private static void ApplyArticlesLocaleTable(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticlesLocale>().HasKey(article => new {article.Id, article.CultureId});
        modelBuilder.Entity<ArticlesLocale>()
            .HasOne(culture => culture.Culture)
            .WithMany(cul => cul.ArticlesTranslates)
            .HasForeignKey(art => art.CultureId);

        modelBuilder.Entity<ArticlesLocale>().Property(artLoc => artLoc.ShortDescription);
        modelBuilder.Entity<ArticlesLocale>().Property(artLoc => artLoc.SubText);
        modelBuilder.Entity<ArticlesLocale>().Property(artLoc => artLoc.Title);
        modelBuilder.Entity<ArticlesLocale>().Property(artLoc => artLoc.Content);
    }

    private static void ApplyCultureTable(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Culture>().HasKey(culture => culture.Id);
        modelBuilder.Entity<Culture>().Property(culture => culture.Name).IsRequired();
        modelBuilder.Entity<Culture>().Property(culture => culture.DisplayedName).IsRequired();

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.ArticlesTranslates)
            .WithOne(a => a.Culture)
            .HasForeignKey(cul => cul.CultureId);

        modelBuilder.Entity<Culture>()
            .HasMany(cul => cul.Categories)
            .WithOne(a => a.Culture)
            .HasForeignKey(cul => cul.CultureId);
    }

    private static void ApplyCategoriesLocaleTable(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryLocale>().HasKey(ct => new {ct.CategoryId, ct.CultureId});
        modelBuilder.Entity<CategoryLocale>().HasKey(culture => culture.CategoryId);
        modelBuilder.Entity<CategoryLocale>().Property(culture => culture.Name);
        modelBuilder.Entity<CategoryLocale>()
            .HasOne(culture => culture.Culture)
            .WithMany(cul => cul.Categories)
            .HasForeignKey(cul => cul.CultureId);
    }
}